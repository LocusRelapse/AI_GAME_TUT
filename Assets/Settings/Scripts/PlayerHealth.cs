using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Текущее здоровье игрока
    public int currentHealth = 100;

    // Метод получения урона
    public void TakeDamage(int damage)
    {
        // Уменьшаем здоровье
        currentHealth -= damage;

        // Выводим текущее здоровье в консоль
        Debug.Log("Health: " + currentHealth);

        // Проверяем смерть игрока
        if (currentHealth <= 0)
        {
            Debug.Log("Игрок мёртв");
        }
    }
}
