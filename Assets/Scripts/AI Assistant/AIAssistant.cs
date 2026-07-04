using UnityEngine;

public class AIAssistant : MonoBehaviour
{
    [SerializeField]
    private BuffInformationUI buffInformationUI;

    public void OnShowBuff()
    {
        buffInformationUI.ShowBuffInformation(BuffManager.Instance.SelectedBuff);
    }

    public void OnHideBuff() 
    {
        buffInformationUI.HideBuffInformation();
    }
}
