using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header(" Data ")]
    private int coins;
    private const string coinsKey = "Coins";

    [Header(" Events ")]
    public static Action onCoinsUpdated;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetCoins()
    {
        return coins;
    }

    private void LoadData()
    {
        coins = PlayerPrefs.GetInt(coinsKey);

        onCoinsUpdated?.Invoke();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(coinsKey, coins);
    }
}
