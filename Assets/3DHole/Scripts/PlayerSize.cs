using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        fillImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void IncreaseScale()
    {
        float targetScale = transform.localScale.x + scaleStep;
        Vector3 scaleVector = new Vector3(targetScale, 1f, targetScale);
        LeanTween.scale(transform.gameObject, scaleVector, animationTime * Time.deltaTime * 60)
            .setEase(sizeCurve);

    }
    public void CollectibleCollected(float objectSize)
    {
        scaleValue += objectSize;

        if (scaleValue >= scaleIncreaseThreshold)
        {
            IncreaseScale();
            scaleValue = scaleValue % scaleIncreaseThreshold;
        }

        UpdateFillDisplay();
    }

    private void UpdateFillDisplay()
    {
        float targetFillAmount = scaleValue / scaleIncreaseThreshold;

        /*
        LeanTween.value(fillImage.fillAmount, targetFillAmount, .2f * Time.deltaTime * 60).
            setOnUpdate(UpdateFillDisplaySmoothly);
        */

        // This method is better than the commented
        LeanTween.value(fillImage.fillAmount, targetFillAmount, .2f * Time.deltaTime * 60).
            setOnUpdate((value) => fillImage.fillAmount = value);
    }
    /*
    private void UpdateFillDisplaySmoothly(float value)
    {
        fillImage.fillAmount = value;
    }*/
}
