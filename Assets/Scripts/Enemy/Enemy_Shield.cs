using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shield : MonoBehaviour
{


    [Header("Impact details")]
    [SerializeField] private Material shieldMaterial;
    [SerializeField] private float defaultShieldGlow = 1;
    [SerializeField] private float impactShieldGlow = 3;
    [SerializeField] private float impactScaleMultiplier = .97f;
    [SerializeField] private float impactSpeed = .1f;
    [SerializeField] private float impactResetDuration = .5f;


    private float defualtScale;
    private string shieldFresnelParameter = "_FresnelPower";
    private Coroutine currentCo;

    void Start()
    {
        defualtScale = transform.localScale.x;
    }

    public void ActivateShieldImpact()
    {
        if (currentCo != null)
            StopCoroutine(currentCo);

        currentCo = StartCoroutine(ImpactCo());
    }

    private IEnumerator ImpactCo()
    {
        yield return StartCoroutine(ShieldChangeCo(impactShieldGlow, defualtScale * impactScaleMultiplier, impactSpeed));

        StartCoroutine(ShieldChangeCo(defaultShieldGlow, defualtScale, impactResetDuration));
    }

    private IEnumerator ShieldChangeCo(float targetGlow, float targetScale, float duration)
    {
        float time = 0;
        float startGlow = shieldMaterial.GetFloat(shieldFresnelParameter);
        Vector3 initialScale = transform.localScale;
        Vector3 newTargetScale = new Vector3(targetScale, targetScale, targetScale);

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, newTargetScale, time / duration);

            float neGlow = Mathf.Lerp(startGlow, targetGlow, time / duration);
            shieldMaterial.SetFloat(shieldFresnelParameter, neGlow);

            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = newTargetScale;
        shieldMaterial.SetFloat(shieldFresnelParameter, targetGlow);
    }
}
