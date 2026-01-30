using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class Collectible : MonoBehaviour
{
    public int scoreValue = 1;

    private AudioSource audioSource;
    private bool collected = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;

        // Увеличиваем счёт
        GameManager.Score += scoreValue;

        // Проигрываем звук, если он есть
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
