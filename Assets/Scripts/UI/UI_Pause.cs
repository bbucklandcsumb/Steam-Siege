using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : MonoBehaviour
{

    private UI ui;
    private UI_InGame inGameUI;

    [SerializeField] private GameObject[] pauseUIElements;

    void Awake()
    {
        ui = GetComponentInParent<UI>();
        inGameUI = ui.GetComponentInChildren<UI_InGame>(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            ui.SwitchTo(inGameUI.gameObject);
        }
    }

    public void SwitchPauseUIElements(GameObject elementsToEnable)
    {
        foreach (GameObject obj in pauseUIElements)
        {
            obj.SetActive(false);
        }

        elementsToEnable.SetActive(true);
    }

    private void OnEnable()
    {
        Time.timeScale = 0; // Pause the game      
    }

    private void OnDisable()
    {
        Time.timeScale = 1; // Resume the game
    }
}
