using UnityEngine;
using TMPro;

public class BuffInformationUI : MonoBehaviour
{
    [SerializeField] private TMP_Text buffInformationTitleTXT;
    [SerializeField] private TMP_Text buffInformationDescTXT;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on BuffInformationUI.");
        }
    }

    public void ShowBuffInformation(BuffSO buff)
    {
        if (buff == null)
        {
            Debug.LogError("BuffSO is null. Cannot show buff information.");
            return;
        }

        buffInformationTitleTXT.text = buff.buffName;
        buffInformationDescTXT.text = buff.buffDescription;

        if (_canvasGroup != null)
        {
            _canvasGroup.alpha = 1f; // Make the UI visible
        }
    }

    public void HideBuffInformation()
    {
        if (_canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on BuffInformationUI.");
            return;
        }

        _canvasGroup.alpha = 0f; // Make the UI invisible
    }
}
