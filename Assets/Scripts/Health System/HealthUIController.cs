using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private Transform containerNormalHeart;
    private List<Image> _normalHeartIcons = new List<Image>();

    [SerializeField] private Transform containerExtraHeart;
    private List<Image> _extraHeartIcons = new List<Image>();

    [Header("Sprites and prefabs")]
    [SerializeField] private GameObject normalHeartPrefab;
    [SerializeField] private Sprite fullNormalHeartSprite;
    [SerializeField] private Sprite emptyNormalHeartSprite;

    [SerializeField] private GameObject extraHeartPrefab;
    [SerializeField] private Sprite fullExtraHeartSprite;
    [SerializeField] private Sprite emptyExtraHeartSprite;

    public void InitializeNormalHearts(int maxHealth)
    {
        foreach (Transform child in containerNormalHeart)
            Destroy(child.gameObject);
        _normalHeartIcons.Clear();

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(normalHeartPrefab, containerNormalHeart);
            Image heartImage = heart.GetComponent<Image>();
            heartImage.sprite = fullNormalHeartSprite;
            _normalHeartIcons.Add(heartImage);
        }
    }

    public void UpdateNormalHealthUI(int currentHealth)
    {
        for (int i = 0; i < _normalHeartIcons.Count; i++)
        {
            _normalHeartIcons[i].sprite = i < currentHealth ? fullNormalHeartSprite : emptyNormalHeartSprite;
        }
    }

    public void InitializeExtraHearts(int extraHeart)
    {
        foreach (Transform child in containerExtraHeart)
            Destroy(child.gameObject);
        _extraHeartIcons.Clear();

        for (int i = 0; i < extraHeart; i++)
        {
            GameObject heart = Instantiate(extraHeartPrefab, containerExtraHeart);
            Image heartImage = heart.GetComponent<Image>();
            heartImage.sprite = fullExtraHeartSprite;
            _extraHeartIcons.Add(heartImage);
        }
    }

    public void UpdateExtraHealthUI(int currentExtraHealth)
    {
        for (int i = 0; i < _extraHeartIcons.Count; i++)
        {
            _extraHeartIcons[i].sprite = i < currentExtraHealth ? fullExtraHeartSprite : emptyExtraHeartSprite;
        }
    }

    public void ShowExtraHeart()
    {
        containerExtraHeart.gameObject.SetActive(true);
    }
    public void HideExtraHeart()
    {
        containerExtraHeart.gameObject.SetActive(false);
    }
}
