using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private Button timerButton;
    [SerializeField] private Button sizeButton;
    [SerializeField] private Button powerButton;

    [Header(" Data ")]
    private int timerLevel;
    private int sizeLevel;
    private int powerLevel;


    private const string timerKey = "TimerLevel";
    private const string sizeKey = "SizeLevel";
    private const string powerKey = "PowerLevel";


    [Header(" Events ")]
    public static Action onTimerPurchased;
    public static Action onSizePurchased;
    public static Action onPowerPurchased;

    private void Awake()
    {
        LoadData();
        InitializeButtons();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeButtons()
    {
        // A script attached to each upgrade button
        // Call this script and configure
        // Upgrade level
        // Interactability

        UpdateButtonsVisuals();
    }

    private void UpdateButtonsVisuals()
    {
        timerButton.GetComponent<UpgradeButton>().Configure(timerLevel, 100);
        sizeButton.GetComponent<UpgradeButton>().Configure(sizeLevel, 150);
        powerButton.GetComponent<UpgradeButton>().Configure(powerLevel, 300);
    }

    public void TimerButtonCallback()
    {
        onTimerPurchased?.Invoke();

        timerLevel++;
        SaveData();
        UpdateButtonsVisuals();
    }

    public void SizeButtonCallback()
    {
        onSizePurchased?.Invoke();

        sizeLevel++;
        SaveData();
        UpdateButtonsVisuals();
    }

    public void PowerButtonCallback()
    {
        onPowerPurchased?.Invoke();

        powerLevel++;
        SaveData();
        UpdateButtonsVisuals();
    }

    private void LoadData()
    {
        timerLevel = PlayerPrefs.GetInt(timerKey);
        sizeLevel = PlayerPrefs.GetInt(sizeKey);
        powerLevel = PlayerPrefs.GetInt(powerKey);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(timerKey, timerLevel);
        PlayerPrefs.SetInt(sizeKey, sizeLevel);
        PlayerPrefs.SetInt(powerKey, powerLevel);
    }

}
