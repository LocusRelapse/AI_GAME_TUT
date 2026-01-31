using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Синглтон
    public static GameManager Instance;

    // Игровые данные
    public int currentScore = 0;
    public int playerLives = 3;

    // UI
    public TMP_Text scoreText;
    public TMP_Text livesText;

    private void Awake()
{
    Debug.Log("GameManager Awake: " + gameObject.name);

    if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
        return;
    }

    Instance = this;

    UpdateUI();
}


    private void Start()
    {
        // Обновляем UI при старте сцены
        UpdateUI();
    }

    // Добавление очков
    public void AddScore(int value)
    {
        Debug.Log("AddScore вызван у: " + gameObject.name);

        currentScore += value;
        UpdateUI();
    }

    // Потеря жизни
    public void LoseLife()
    {
        playerLives--;
        UpdateUI();
    }

    // Обновление UI
    public void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;

        if (livesText != null)
            livesText.text = "Lives: " + playerLives;
    }

    // Заглушка под будущую логику
    public void GameOver()
    {
        Debug.Log("GameManager.GameOver() вызван");
    }
}
