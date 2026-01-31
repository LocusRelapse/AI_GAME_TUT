using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Метод получения урона
    public void TakeDamage(int damage)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager.Instance не найден!");
            return;
        }

        // Теряем одну жизнь при каждом уроне
        GameManager.Instance.LoseLife();

        Debug.Log("Lives: " + GameManager.Instance.playerLives);

        // Проверка Game Over
        if (GameManager.Instance.playerLives <= 0)
        {
            Debug.Log("Game Over");
            GameManager.Instance.GameOver();
        }
    }
}
