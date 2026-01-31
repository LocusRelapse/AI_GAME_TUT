using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class Collectible : MonoBehaviour
{
    // Событие, вызываемое при добавлении очков
    // Передаёт количество полученных очков
    public static event Action<int> OnScoreAdded;

    // Сколько очков даёт предмет
    public int scoreValue = 10;

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

        // Сообщаем о получении очков через событие
        OnScoreAdded?.Invoke(scoreValue);

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
