using TMPro;
using UnityEngine;

public class UI_InGame : MonoBehaviour
{
    private UI ui;
    private UI_Pause pauseUI;
    private UI_Animator uiAnimator;

    [SerializeField] private TextMeshProUGUI healthPointsText;
    [SerializeField] private TextMeshProUGUI currencyText;
    [Space]
    [SerializeField] private TextMeshProUGUI waveTimerText;
    [SerializeField] private float waveTimerOffset;
    [SerializeField] private UI_TextBlinkEffect waveTimerBlinkEffect;

    void Awake()
    {
        uiAnimator = GetComponentInParent<UI_Animator>();
        ui = GetComponentInParent<UI>();
        pauseUI = ui.GetComponentInChildren<UI_Pause>(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            ui.SwitchTo(pauseUI.gameObject);
        }
    }

    public void ShakeCurrencyUI() => ui.uiAnim.Shake(currencyText.transform.parent as RectTransform);
    public void ShakeHealthPointsUI() => ui.uiAnim.Shake(healthPointsText.transform.parent as RectTransform);



    public void UpdateHealthPointsUI(int value, int maxValue)
    {
        int newValue = maxValue - value;
        healthPointsText.text = "Threat : " + newValue + "/" + maxValue;
    }

    public void UpdateCurrencyUI(int value)
    {
        currencyText.text = "resources : " + value;
    }

    public void UpdateWaveTimerUI(float value) => waveTimerText.text = "seconds : " + value.ToString("00");
    public void EnableWaveTimer(bool enable)
    {
        Transform waveTimerTransform = waveTimerText.transform.parent;
        float yOffset = enable ? -waveTimerOffset : waveTimerOffset;

        Vector3 offset = new Vector3(0, yOffset);


        uiAnimator.ChangePosition(waveTimerTransform, offset);
        waveTimerBlinkEffect.EnableBlink(enable);
    }

    public void ForceWaveButton()
    {
        WaveManager waveManager = FindFirstObjectByType<WaveManager>();
        waveManager.ForceNextWave();
    }
}
