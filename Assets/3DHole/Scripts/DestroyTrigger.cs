using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PlayerSize playerSize;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Collectible collectible))
        {
            playerSize.CollectibleCollected(collectible.GetSize());

            int coinDropAmount = collectible.GetCoinDropAmount();

            // Drop coins
            for (int i = 0; i < coinDropAmount; i++)
            {
                DropCoin();
            }

            Destroy(other.gameObject);
        }
    }

    private void DropCoin()
    {
        DataManager.instance.AddCoins(1);
    }
}
