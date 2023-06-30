using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradesManager : MonoBehaviour
{
    [Header (" Events ")]
    public static Action onTimerPurchased;
    public static Action onSizePurchased;
    public static Action onPowerPurchased;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimerButtonCallback()
    {
        onTimerPurchased?.Invoke();
    }

    public void SizeButtonCallback()
    {
        onSizePurchased?.Invoke();
    }

    public void PowerButtonCallback()
    {
        onPowerPurchased?.Invoke();
    }

}
