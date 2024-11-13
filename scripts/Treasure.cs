using System;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public event Action OnCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        OnCollected?.Invoke();
    }
}
