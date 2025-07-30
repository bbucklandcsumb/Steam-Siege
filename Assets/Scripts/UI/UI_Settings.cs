using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    private CameraController camController;

    [Header("Keyboard Sensetivity")]
    [SerializeField] private Slider keyboardSensSlider;
    [SerializeField] private TextMeshProUGUI keyboardSensText;
    [SerializeField] private string keyboardSensParameter = "keyboardSens";

    [SerializeField] private float minKeyboardSens = 100;
    [SerializeField] private float maxKeyboardSens = 500;

    [Header("Mouse Sensetivity")]
    [SerializeField] private Slider mouseSensSlider;
    [SerializeField] private TextMeshProUGUI mouseSensText;
    [SerializeField] private string mouseSensParameter = "mouseSens";

    [SerializeField] private float minMouseSens = 100;
    [SerializeField] private float maxMouseSens = 500;


    void Awake()
    {
        camController = FindFirstObjectByType<CameraController>();
    }

    public void KeyboardSensitivity(float value)
    {
        float newSensitivity = Mathf.Lerp(minKeyboardSens, maxKeyboardSens, value);
        camController.AdjustKeyboardSensitivity(newSensitivity);

        keyboardSensText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void MouseSensitivity(float value)
    {
        float newSensitivity = Mathf.Lerp(minMouseSens, maxMouseSens, value);
        camController.AdjustMouseSensitivity(newSensitivity);

        mouseSensText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(keyboardSensParameter, keyboardSensSlider.value);
        PlayerPrefs.SetFloat(mouseSensParameter, mouseSensSlider.value);
    }

    private void OnEnable()
    {
        keyboardSensSlider.value = PlayerPrefs.GetFloat(keyboardSensParameter, .5f);
        mouseSensSlider.value = PlayerPrefs.GetFloat(mouseSensParameter, .5f);
    }
}
