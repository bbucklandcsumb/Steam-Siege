using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_TextBlinkEffect : MonoBehaviour
{
    private TextMeshProUGUI myText;

    [SerializeField] private float changeValueSpeed;
    private float targetAlpha;
    private bool canBlink;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!canBlink)
            return;

        if (Mathf.Abs(myText.color.a - targetAlpha) > 0.01f)
        {
            // Smoothly transition the alpha value of the text
            float newAlpha = Mathf.Lerp(myText.color.a, targetAlpha, changeValueSpeed * Time.deltaTime);
            ChangeColorAlpha(newAlpha);
        }
        else
        {
            ChangeTargetAlpha();
        }
    }

    public void EnableBlink(bool enable)
    {
        canBlink = enable;

        if (canBlink == false)
            ChangeColorAlpha(1);
    }

    private void ChangeTargetAlpha() => targetAlpha = (targetAlpha == 1) ? 0 : 1;
    

    private void ChangeColorAlpha(float newAlpha)
    {
        Color myColor = myText.color;
        myText.color = new Color(myColor.r, myColor.g, myColor.b, newAlpha);
    }
}
