using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Treasure treasure;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text winText;

    private int _score = 0;


    private void OnEnable()
    {
        Collectible.OnCollected += OnCollectCollectible;
        playerController.OnHealthChanged += OnHealthChanged;
        treasure.OnCollected += Treasure_OnCollected;
    }

    private void OnDisable()
    {
        Collectible.OnCollected -= OnCollectCollectible;
        playerController.OnHealthChanged -= OnHealthChanged;
        treasure.OnCollected -= Treasure_OnCollected;
    }

    private void Treasure_OnCollected()
    {
        winText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnCollectCollectible(int scoreAdd)
    {
        _score += scoreAdd;
        scoreText.text = "Score: " + _score;
    }

    private void OnHealthChanged(int newHealth)
    {
        slider.value = newHealth;
    }
}
