using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private int scoreAmount = 100;

    public static event Action<int> OnCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        OnCollected?.Invoke(scoreAmount);
        Destroy(gameObject);
    }
}
