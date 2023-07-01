using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerSize : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image fillImage;

    [Header(" Settings ")]
    [Tooltip("Increase the limit of size of blocks eaten to increase the size")]
    [SerializeField] private float scaleIncreaseThreshold;

    [Tooltip("Increase the size of the player multiplier")]
    [SerializeField] private float scaleStep;

    [Tooltip("Change the growing up animation curve")]
    [SerializeField] private AnimationCurve sizeCurve;

    [Tooltip("Increase the time for growing up animation")]
    [SerializeField] private float animationTime;

    private float scaleValue;

    [Header(" Power ")]
    private float powerMultiplier;

    [Header(" Events ")]
    public static Action<float> onIncrease;
    public Action<float> onIncreaseMoveSpeed;

    private void Awake()
    {
        UpgradesManager.onDataLoaded += UpgradesDataLoadedCallback;
    }
    // Start is called before the first frame update
    void Start()
    {
        fillImage.fillAmount = 0;

        UpgradesManager.onSizePurchased += SizePurchasedCallback;
        UpgradesManager.onPowerPurchased += PowerPurchasedCallback;
    }

    private void OnDestroy()
    {
        UpgradesManager.onSizePurchased -= SizePurchasedCallback;
        UpgradesManager.onPowerPurchased -= PowerPurchasedCallback;
        UpgradesManager.onDataLoaded -= UpgradesDataLoadedCallback;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void IncreaseScale()
    {
        float targetScale = transform.localScale.x + scaleStep;
        UpdateScale(targetScale);
    }

    private void UpdateScale(float targetScale)
    {
        Vector3 scaleVector = new Vector3(targetScale, 1f, targetScale);
        LeanTween.scale(transform.gameObject, scaleVector, animationTime * Time.deltaTime * 60)
            .setEase(sizeCurve);

        onIncrease?.Invoke(targetScale);
        onIncreaseMoveSpeed?.Invoke(targetScale);
    }

    public void CollectibleCollected(float objectSize)
    {
        scaleValue += objectSize * (1 + powerMultiplier);

        if (scaleValue >= scaleIncreaseThreshold)
        {
            IncreaseScale();
            scaleValue = scaleValue % scaleIncreaseThreshold;

            onIncreaseMoveSpeed?.Invoke(transform.localScale.x + scaleStep);
        }

        UpdateFillDisplay();
    }

    private void UpdateFillDisplay()
    {
        float targetFillAmount = scaleValue / scaleIncreaseThreshold;

        LeanTween.value(fillImage.fillAmount, targetFillAmount, .2f * Time.deltaTime * 60).
            setOnUpdate((value) => fillImage.fillAmount = value);
    }

    private void SizePurchasedCallback()
    {
        IncreaseScale();
    }

    private void PowerPurchasedCallback()
    {
        powerMultiplier++;
    }

    private void UpgradesDataLoadedCallback(int timerLevel, int sizeLevel, int powerLevel)
    {
        float targetScale = transform.localScale.x + scaleStep * sizeLevel;
        UpdateScale(targetScale);

        powerMultiplier = powerLevel;
    }

}
