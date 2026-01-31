using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class Collectible : MonoBehaviour
{
    // Сколько очков даёт предмет
    public int scoreValue = 1;

    private AudioSource audioSource;
    private bool collected = false;

    private void Awake()
    {
        // Получаем AudioSource на объекте
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Защита от повторного срабатывания
        if (collected) return;

        // Проверяем, что вошёл игрок
        if (!other.CompareTag("Player")) return;

        collected = true;

        // Увеличиваем счёт ЧЕРЕЗ GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }
        else
        {
            Debug.LogWarning("GameManager.Instance не найден!");
        }

        // Проигрываем звук и уничтожаем объект
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
