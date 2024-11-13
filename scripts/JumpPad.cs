using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private GameObject[] _torches;
    
    private void Awake()
    {
        _torches = GameObject.FindGameObjectsWithTag("Torch");
        foreach (var torch in _torches)
        {
            torch.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        collision.GetComponent<PlayerController>().TakeDamage(-25);

        foreach (var torch in _torches)
        {
            if (torch != null) torch.SetActive(true);
        }

        var dangerousObjects = GameObject.FindGameObjectsWithTag("Dangerous");
        foreach (var dangerousObject in dangerousObjects)
        {
            dangerousObject.SetActive(false);
        }
    }
}
