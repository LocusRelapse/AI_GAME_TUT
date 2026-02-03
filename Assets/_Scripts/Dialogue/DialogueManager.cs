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

    private DialogueData currentDialogue;
    private int currentNodeIndex;

    void Awake()
    {
        // ВАЖНО: именно Awake, а не Start
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

        nameText.text = node.characterName;
        dialogueText.text = node.dialogueText;
        portraitImage.sprite = node.characterPortrait;
    
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
        dialoguePanel.SetActive(false);
        currentDialogue = null;
    }
}
