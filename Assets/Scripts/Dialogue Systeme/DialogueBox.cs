using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private KeywordHighlightSO keywordHighlights;

    public void ShowDialogueBox()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HideDialogueBox()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void UpdateDialogueBox(DialogueLine dialogueData)
    {
        characterName.text = dialogueData.nameCharacter;

        // Cukup panggil teksnya saja tanpa mengoper keywordHighlights
        dialogueText.text = DialogueHighlighter.ApplyHighlights(dialogueData.dialogueText);

        ShowDialogueBox();
    }
}
