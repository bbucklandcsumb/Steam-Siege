using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_BuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UI ui;
    private BuildManager buildManager;
    private CameraEffects cameraEffects;
    private GameManager gameManager;
    private UI_BuildButtonsHolder buildButtonsHolder;
    private UI_BuildButtonsOnHoverEffect onHoverEffect;

    [SerializeField] private string towerName;
    [SerializeField] private int towerPrice = 50;
    [Space]
    [SerializeField] private GameObject towerToBuild;
    [SerializeField] private float towerCenterY = 0.5f;
    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI towerNameText;
    [SerializeField] private TextMeshProUGUI towerPriceText;

    

    private TowerPreview towerPreview;
    public bool buttonUnlocked { get; private set; }

    void Awake()
    {
        ui = GetComponentInParent<UI>();
        onHoverEffect = GetComponent<UI_BuildButtonsOnHoverEffect>();
        buildButtonsHolder = GetComponentInParent<UI_BuildButtonsHolder>();
        buildManager = FindFirstObjectByType<BuildManager>();
        cameraEffects = FindFirstObjectByType<CameraEffects>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {
        CreateTowerPreview();
    }

    private void CreateTowerPreview()
    {
        GameObject newPreview = Instantiate(towerToBuild, Vector3.zero, Quaternion.identity);

        towerPreview = newPreview.AddComponent<TowerPreview>();
        towerPreview.gameObject.SetActive(false);
    }

    public void SelectButton(bool select)
    {
        //if (towerPreview == null)
            //return;

        BuildSlot slotToUse = buildManager.GetSelectedSlot();

        if (slotToUse == null)
            return;

        Vector3 previewPosition = slotToUse.GetBuildPosition(1);

        towerPreview.gameObject.SetActive(select);
        towerPreview.ShowPreview(select, previewPosition);
        onHoverEffect.ShowcaseButton(select);
        buildButtonsHolder.SetLastSelected(this);
    }

    public void UnlockTowerIfNeeded(string towerNameToCheck, bool unlockStatus)
    {
        if (towerNameToCheck != towerName)
            return;

        buttonUnlocked = unlockStatus;
        gameObject.SetActive(unlockStatus);
    }

    public void BuildTower()
    {
        if (!gameManager.HasEnoughCurrency(towerPrice))
        {
            ui.inGameUI.ShakeCurrencyUI();
            return;
        }


        if (towerToBuild == null)
        {
            Debug.LogWarning("Tower to build is not assigned.");
            return;
        }

        if (ui.buildButtonsUI.GetLastSelectedButton() == null)
            return;


        BuildSlot slotToUse = buildManager.GetSelectedSlot();
        buildManager.CancelBuildAction();
        slotToUse.SnapToDefaultPositionImmediately();
        slotToUse.SetSlotAvailableTo(false);
        ui.buildButtonsUI.SetLastSelected(null);
        cameraEffects.Screenshake(0.15f, 0.02f);

        GameObject newTower = Instantiate(towerToBuild, slotToUse.GetBuildPosition(towerCenterY), Quaternion.identity);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        buildManager.MouseOverUI(true);

        foreach (var button in buildButtonsHolder.GetBuildButtons())
        {
            if(button.gameObject.activeSelf)
                button.SelectButton(false);
        }

        SelectButton(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buildManager.MouseOverUI(false);
    }

    private void OnValidate()
    {
        towerNameText.text = towerName;
        towerPriceText.text = towerPrice + "";
        gameObject.name = "BuildButton_UI - " + towerName;
    }

}
