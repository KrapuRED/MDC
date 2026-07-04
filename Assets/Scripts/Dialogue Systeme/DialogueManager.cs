using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueDataSO dialogueData;

    [SerializeField] private DialogueHUD dialogueHUD;
    [SerializeField] private DialogueBox playerDialogueBox;
    [SerializeField] private DialogueBox npcDialogueBox;

    [SerializeField] private Computer computer;

    private List<DialogueLine> _dialogueLines = new();
    private int _dialogueCount = -1;
    
    private bool isDialogueActive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (dialogueData != null)
            _dialogueLines = dialogueData.dialogueList;
    }

    private void ContinueDialogue()
    {
        _dialogueCount++;

        if (_dialogueCount >= _dialogueLines.Count)
        {
            Debug.Log($"{dialogueData.name} is done, you can go to work!");
            dialogueHUD.HideDialogueHUD();

            npcDialogueBox.EndDialogue();
            computer.TurnOnComputer();

            return;
        }

        DialogueLine nextDialogue = _dialogueLines[_dialogueCount];

        NextDialogue(nextDialogue);
    }

    private void NextDialogue(DialogueLine nextDialogue)
    {
        if (nextDialogue.charType == CharacterType.Player)
        {
            Debug.Log($"Name : {nextDialogue.nameCharacter} Dialogue Line : {nextDialogue.dialogueText}");
            npcDialogueBox.HideDialogueBox();
            playerDialogueBox.UpdateDialogueBox(nextDialogue);
        }
        else
        {
            playerDialogueBox.HideDialogueBox();
            npcDialogueBox.UpdateDialogueBox(nextDialogue);
        }
    }

    public void TriggerDialogue()
    {
        if (isDialogueActive)
            return;

        if (dialogueData == null)
            return;

        Debug.Log($"start dialogue, {dialogueData.name} with total dialogue {dialogueData.dialogueList.Count}");
        dialogueHUD.ShowDialogueHUD();
        isDialogueActive = true;
        ContinueDialogue();
    }

    public void OnPressContinue()
    {
        ContinueDialogue();
    }
}
