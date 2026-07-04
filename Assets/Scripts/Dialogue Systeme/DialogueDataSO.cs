using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public enum CharacterType
{
    Player,
    Alice,
    jackson,
    AIAssitance,
    AISupervisor,
    AIBoss
}

[System.Serializable]
public class DialogueLine
{
    public string nameCharacter;
    public CharacterType charType;
    [TextArea(5,10)]
    public string dialogueText;
}

[CreateAssetMenu(fileName = "DialogueDataSO", menuName = "Dialogues/DialogueDataSO")]
public class DialogueDataSO : ScriptableObject
{
    public string dialogueName;
    public List<DialogueLine> dialogueList;
}
