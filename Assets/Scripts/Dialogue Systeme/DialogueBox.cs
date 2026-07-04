using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private KeywordHighlightSO keywordHighlights;

    [SerializeField] private Sprite AliceFace;
    [SerializeField] private Sprite JacksonFace;
    [SerializeField] private Sprite AIAssitance;
    [SerializeField] private Sprite AISupervisor;
    [SerializeField] private Sprite AIBoss;

    [SerializeField] private SpriteRenderer spriteRenderer;

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
        if (dialogueData.charType != CharacterType.Player && spriteRenderer != null)
        {
            spriteRenderer.sprite = dialogueData.charType switch
            {
                CharacterType.Alice => AliceFace,
                CharacterType.jackson => JacksonFace,
                CharacterType.AIAssitance => AIAssitance,
                CharacterType.AISupervisor => AISupervisor,
                CharacterType.AIBoss => AIBoss,
                _ => spriteRenderer.sprite // unknown type - leave whatever was already showing
            };
        }
        
        characterName.text = dialogueData.nameCharacter;

        // Cukup panggil teksnya saja tanpa mengoper keywordHighlights
        dialogueText.text = DialogueHighlighter.ApplyHighlights(dialogueData.dialogueText);

        ShowDialogueBox();
    }

    public void EndDialogue()
    {
        spriteRenderer.sprite = null;
    }
}
