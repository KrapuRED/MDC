using UnityEngine;
using UnityEngine.UI;

public class DropBoxIndiactor : MonoBehaviour
{
    [SerializeField] private GameObject dropBoxIndiactor;

    public void ShowIndicator()
    {
        if (dropBoxIndiactor == null)
            return;

        dropBoxIndiactor.SetActive(true);
    }

    public void HideIndicator()
    {
        if (dropBoxIndiactor == null)
            return;

        dropBoxIndiactor.SetActive(false);
    }
}
