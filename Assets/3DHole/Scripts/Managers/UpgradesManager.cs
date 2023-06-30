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


    [Header(" Princinig ")]
    [SerializeField] private int basePrice;
    [SerializeField] private int priceStep;

    [Header(" Events ")]
    public static Action onTimerPurchased;
    public static Action onSizePurchased;
    public static Action onPowerPurchased;
    public static Action<int, int, int> onDataLoaded;

    private void Awake()
    {
        DataManager.onCoinsUpdated += CoinsUpdateCallback;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        InitializeButtons();
    }

    private void OnDestroy()
    {
        DataManager.onCoinsUpdated -= CoinsUpdateCallback;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void CoinsUpdateCallback()
    {
        UpdateButtonsInteractability();
    }

    private void InitializeButtons()
    {
        UpdateButtonsVisuals();
    }

    private void UpdateButtonsInteractability()
    {
        timerButton.interactable = GetUpgradePrice(timerLevel) <= DataManager.instance.GetCoins();
        sizeButton.interactable = GetUpgradePrice(sizeLevel) <= DataManager.instance.GetCoins();
        powerButton.interactable = GetUpgradePrice(powerLevel) <= DataManager.instance.GetCoins();
    }

    private void UpdateButtonsVisuals()
    {
        timerButton.GetComponent<UpgradeButton>().Configure(timerLevel, GetUpgradePrice(timerLevel));
        sizeButton.GetComponent<UpgradeButton>().Configure(sizeLevel, GetUpgradePrice(sizeLevel));
        powerButton.GetComponent<UpgradeButton>().Configure(powerLevel, GetUpgradePrice(powerLevel));
        UpdateButtonsInteractability();
    }

    public void TimerButtonCallback()
    {
        onTimerPurchased?.Invoke();

        DataManager.instance.Purchase(GetUpgradePrice(timerLevel));

        timerLevel++;

        SaveAndUpdateVisuals();
    }

    public void SizeButtonCallback()
    {
        onSizePurchased?.Invoke();

        DataManager.instance.Purchase(GetUpgradePrice(sizeLevel));

        sizeLevel++;
        SaveAndUpdateVisuals();
    }

    public void PowerButtonCallback()
    {
        onPowerPurchased?.Invoke();

        DataManager.instance.Purchase(GetUpgradePrice(powerLevel));

        powerLevel++;
        SaveAndUpdateVisuals();
    }

    private void SaveAndUpdateVisuals()
    {
        SaveData();
        UpdateButtonsVisuals();
    }

    private int GetUpgradePrice(int upgradeLevel)
    {
        return basePrice + upgradeLevel * priceStep;
    }

    private void LoadData()
    {
        timerLevel = PlayerPrefs.GetInt(timerKey);
        sizeLevel = PlayerPrefs.GetInt(sizeKey);
        powerLevel = PlayerPrefs.GetInt(powerKey);

        // Send an event with the different upgrades levels
        onDataLoaded?.Invoke(timerLevel, sizeLevel, powerLevel);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(timerKey, timerLevel);
        PlayerPrefs.SetInt(sizeKey, sizeLevel);
        PlayerPrefs.SetInt(powerKey, powerLevel);
    }

}
