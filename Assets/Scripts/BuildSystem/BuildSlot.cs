using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    private UI ui;
    private TileAnimator tileAnim;
    private BuildManager buildManager;
    private Vector3 defaultPosition;

    private bool tileCanBeMoved = true;
    public bool buildSlotAvailable = true;

    public Coroutine currentMovementUpCo;
    public Coroutine moveToDefaultCo;

    void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        tileAnim = FindFirstObjectByType<TileAnimator>();
        buildManager = FindFirstObjectByType<BuildManager>();
        defaultPosition = transform.position;
    }

    private void Start()
    {
        if (buildSlotAvailable == false)
            transform.position += new Vector3(0, 0.1f);
    }


    public void SetSlotAvailableTo(bool value) => buildSlotAvailable = value;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (buildSlotAvailable == false)
            return;

        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (buildManager.GetSelectedSlot() == this)
            return;

        buildManager.EnableBuildMenus();
        buildManager.SelectBuildSlot(this);
        MoveTileUp();

        tileCanBeMoved = false;

        ui.buildButtonsUI.GetLastSelectedButton()?.SelectButton(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buildSlotAvailable == false)
            return;


        if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse2))
            return;


        if (!tileCanBeMoved) return;

        MoveTileUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (buildSlotAvailable == false)
            return;

        if (!tileCanBeMoved) return;

        if (currentMovementUpCo != null)
        {
            Invoke(nameof(MoveToDefaultPosition), tileAnim.GetTravelDuration());
        }
        else
            MoveToDefaultPosition();
    }

    public void UnselectTile()
    {
        MoveToDefaultPosition();
        tileCanBeMoved = true;
    }

    private void MoveTileUp()
    {
        Vector3 targetPosition = transform.position + new Vector3(0, tileAnim.GetBuildSlotYOffset(), 0);
        currentMovementUpCo = StartCoroutine(tileAnim.MoveTileCo(transform, targetPosition));
    }

    private void MoveToDefaultPosition()
    {
        moveToDefaultCo = StartCoroutine(tileAnim.MoveTileCo(transform, defaultPosition));
    }

    public void SnapToDefaultPositionImmediately()
    {
        if (moveToDefaultCo != null)
            StopCoroutine(moveToDefaultCo);

        transform.position = defaultPosition;
    }

    public Vector3 GetBuildPosition(float yOffset) => defaultPosition + new Vector3(0, yOffset);

}
