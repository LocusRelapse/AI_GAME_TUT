using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageZone : MonoBehaviour
{
    // Количество урона, которое наносит ловушка
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что в триггер вошёл игрок
        if (!other.CompareTag("Player"))
            return;

        // Пытаемся получить компонент PlayerHealth у игрока
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        // Если компонент найден — наносим урон
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
