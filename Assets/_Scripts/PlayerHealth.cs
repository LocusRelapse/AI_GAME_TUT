using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    // Текущее здоровье игрока (скрытое, изменяется только внутри класса)
    [SerializeField]
    private int currentHealth = 100;

    // Событие, вызываемое при изменении здоровья
    // Передаёт новое значение здоровья
    public static event Action<int> OnHealthChanged;

    // Событие, вызываемое при смерти игрока
    public static event Action OnPlayerDeath;

    // Метод получения урона
    public void TakeDamage(int damage)
    {
        // Уменьшаем здоровье
        currentHealth -= damage;

        // Сообщаем всем подписчикам новое значение здоровья
        OnHealthChanged?.Invoke(currentHealth);

        // Проверяем смерть игрока
        if (currentHealth <= 0)
        {
            Debug.Log("Игрок мёртв");

            // Сообщаем о смерти игрока
            OnPlayerDeath?.Invoke();
        }
    }
}
