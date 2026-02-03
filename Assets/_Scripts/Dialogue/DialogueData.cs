using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogueNode
    {
        public string characterName;
        public Sprite characterPortrait;

        [TextArea(3, 5)]
        public string dialogueText;

        public AudioClip voiceOverClip;
    }

    public DialogueNode[] dialogueNodes;
}
