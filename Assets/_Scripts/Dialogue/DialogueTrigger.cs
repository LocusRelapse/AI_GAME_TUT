using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueToPlay;
    private bool hasPlayed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasPlayed) return;
        if (!other.CompareTag("Player")) return;

        var manager = FindAnyObjectByType<DialogueManager>();
        if (manager == null)
        {
            Debug.LogError("DialogueManager не найден на сцене");
            return;
        }

        manager.StartDialogue(dialogueToPlay);
        hasPlayed = true;
    }
}

