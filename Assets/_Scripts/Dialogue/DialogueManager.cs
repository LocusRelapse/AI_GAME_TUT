using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public Image portraitImage;
    public TMP_Text dialogueText;
    public TMP_Text nameText;

    [Header("Audio")]
    public AudioSource voiceSource; // ТОЛЬКО источник, не клипы

    private DialogueData currentDialogue;
    private int currentNodeIndex;

    void Awake()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (currentDialogue == null) return;
        if (Keyboard.current == null) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShowNextNode();
        }
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (dialogue == null || dialogue.dialogueNodes == null || dialogue.dialogueNodes.Length == 0)
        {
            Debug.LogError("DialogueData пустой или не назначен");
            return;
        }

        currentDialogue = dialogue;
        currentNodeIndex = 0;

        dialoguePanel.SetActive(true);
        ShowNode(currentNodeIndex);
    }

    void ShowNode(int index)
    {
        if (index < 0 || index >= currentDialogue.dialogueNodes.Length)
            return;

        var node = currentDialogue.dialogueNodes[index];

        // UI
        nameText.text = node.characterName;
        dialogueText.text = node.dialogueText;
        portraitImage.sprite = node.characterPortrait;

        // 🔊 VOICE OVER ИЗ НОДЫ
        if (voiceSource != null)
        {
            voiceSource.Stop();

            if (node.voiceOverClip != null)
            {
                voiceSource.clip = node.voiceOverClip;
                voiceSource.Play();
            }
        }
    }

    void ShowNextNode()
    {
        currentNodeIndex++;

        if (currentNodeIndex < currentDialogue.dialogueNodes.Length)
        {
            ShowNode(currentNodeIndex);
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        if (voiceSource != null)
            voiceSource.Stop();

        dialoguePanel.SetActive(false);
        currentDialogue = null;
    }
}
