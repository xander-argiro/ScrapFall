using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text victoryText;
    public TMP_Text gameOverText;

    public TMP_Text keyText;
    public TMP_Text potionText;

    public bool hasKey;

    public void Start()
    {
        hasKey = false;

        victoryText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

    public void Victory()
    {
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();
        audioManager.victorySound.Play();

        victoryText.gameObject.SetActive(true);

        Time.timeScale = 0f; // Pause the game
    }

    public void GameOver()
    {
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();
        audioManager.gameOverSound.Play();

        gameOverText.gameObject.SetActive(true);

        PlayerMovement playerMovement = FindAnyObjectByType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.gameOver = true;
            Debug.Log("Game Over!");
        }
    }
}
