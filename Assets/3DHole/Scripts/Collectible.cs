using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float size;

    // Specify the coin drop amount for each collectible instance
    [SerializeField] private int coinDropAmount = 1;

    private void Start()
    {
        GetComponent<Rigidbody>().sleepThreshold = 0;
    }

    public float GetSize()
    {
        return size;
    }

    public int GetCoinDropAmount()
    {
        return coinDropAmount;
    }
}
