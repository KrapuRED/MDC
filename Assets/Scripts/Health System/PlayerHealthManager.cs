using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager Instance { get; private set; }

    [SerializeField] private int maxHealth;
    [SerializeField] private int extraHealth;
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
        currentHealth = maxHealth;

        if (currentHealth <= maxHealth)
            healthUIController.HideExtraHeart();

        healthUIController.InitializeNormalHearts(maxHealth);
    }

    public void OnGetExtraHeart()
    {
        healthUIController.InitializeNormalHearts(maxHealth);
        healthUIController.InitializeExtraHearts(extraHealth);

        currentHealth = maxHealth + extraHealth;
    }

    public void OnTakingDamage()
    {
        currentHealth -= 1;

        if (currentHealth > maxHealth)
        {
            healthUIController.UpdateExtraHealthUI(currentHealth);
        }
        else
        {
            healthUIController.HideExtraHeart();
            healthUIController.UpdateNormalHealthUI(currentHealth);
        }
    }
}
