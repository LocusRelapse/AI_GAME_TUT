using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Синглтон
    public static GameManager Instance;

    // UI
    public TMP_Text healthText;
    public TMP_Text scoreText;

    // Счёт
    private int currentScore = 0;

    // Сколько карточек нужно для победы
    public int cardsToWin = 5;

    private void Awake()
    {
        // Логика синглтона
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Подписка на события здоровья игрока
        PlayerHealth.OnHealthChanged += UpdateHealthUI;
        PlayerHealth.OnPlayerDeath += HandleGameOver;

        // Подписка на событие сбора очков
        Collectible.OnScoreAdded += AddScore;
    }

    private void OnDestroy()
    {
        // Отписка от событий
        PlayerHealth.OnHealthChanged -= UpdateHealthUI;
        PlayerHealth.OnPlayerDeath -= HandleGameOver;
        Collectible.OnScoreAdded -= AddScore;
    }

    // Обновление UI здоровья
    private void UpdateHealthUI(int newHealth)
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + newHealth;
        }
    }

    // Добавление очков и обновление UI
    private void AddScore(int points)
    {
        currentScore += points;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }

        Debug.Log("Score: " + currentScore);

        if (currentScore >= cardsToWin)
        {
            HandleWin();
        }
    }

    // Проигрыш
    private void HandleGameOver()
    {
        Debug.Log("===== GAME OVER =====");
        Invoke("RestartLevel", 2f);
    }

    // Победа
    private void HandleWin()
    {
        Debug.Log("===== YOU WIN! =====");
        Invoke("RestartLevel", 2f);
    }

    // Перезапуск текущей сцены
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
