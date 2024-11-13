using UnityEngine;

public class DangerousObject : MonoBehaviour
{
    [SerializeField]
    private int damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        var playerController = collision.gameObject.GetComponent<PlayerController>();
        playerController.TakeDamage(damageAmount);
    }
}
