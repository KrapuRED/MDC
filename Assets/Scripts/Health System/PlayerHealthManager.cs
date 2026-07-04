using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager Instance { get; private set; }

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentExtraHealth;
    [SerializeField] private int currentHealth;

    [Header("Health UI Controller")]
    [SerializeField] private HealthUIController healthUIController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (currentHealth > 0)
            return;

        OnInitializeHeart();
    }

    private void OnInitializeHeart()
    {
        currentHealth = maxHealth;

        if (currentHealth <= maxHealth)
            healthUIController.HideExtraHeart();

        healthUIController.InitializeNormalHearts(maxHealth);
    }

    public void OnGetExtraHeart(int extraHealth)
    {
        if (extraHealth <= 0)
        {
            OnInitializeHeart();
            return;
        }

        healthUIController.InitializeNormalHearts(maxHealth);
        healthUIController.InitializeExtraHearts(extraHealth);

        currentExtraHealth = extraHealth;
        currentHealth = maxHealth + extraHealth;
    }

    public void OnTakingDamage()
    {
        if (currentHealth > maxHealth)
        {
            currentExtraHealth -= 1;

            healthUIController.UpdateExtraHealthUI(currentExtraHealth);
        }
        else
        {
            currentHealth -= 1;

            healthUIController.HideExtraHeart();
            healthUIController.UpdateNormalHealthUI(currentHealth);
        }
    }
}
