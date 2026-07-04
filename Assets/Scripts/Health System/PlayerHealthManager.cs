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
        // SELEKSI 2: Jika data di GameManager 0 (atau belum ada), cek apakah Inspector punya nilai
        if (currentHealth > 0)
        {
            // Biarkan currentHealth memakai nilai yang sudah ada sekarang (tidak di-override)
            InitializeUI();
        }
        // SELEKSI 3: Jika dua kondisi di atas tidak terpenuhi, baru set ke MaxHealth
        else
        {
            currentHealth = maxHealth;
            InitializeUI();
        }
    }

    private void InitializeUI()
    {
        if (currentHealth <= maxHealth)
        {
            healthUIController.HideExtraHeart();
        }

        healthUIController.InitializeNormalHearts(maxHealth);
        healthUIController.UpdateNormalHealthUI(currentHealth); // Memastikan UI sinkron dengan angka sekarang
    }

    public void OnGetExtraHeart(int extraHealth)
    {
        if (extraHealth <= 0)
        {
            InitializeUI();
            return;
        }

        healthUIController.InitializeNormalHearts(maxHealth);
        healthUIController.InitializeExtraHearts(extraHealth);

        currentExtraHealth = extraHealth;
        currentHealth = maxHealth + extraHealth;
    }

    public void OnTakingDamage()
    {
        if (currentExtraHealth > 0)
        {
            currentExtraHealth -= 1;
            currentHealth -= 1; // keep combined total in sync (fixes the earlier bug too)
            healthUIController.UpdateExtraHealthUI(currentExtraHealth);
        }
        else
        {
            currentHealth -= 1;

            if (currentHealth <= 0)
            {
                GameManager.Instance.PlayerLosing();
            }

            healthUIController.HideExtraHeart();
            healthUIController.UpdateNormalHealthUI(currentHealth);
        }
    }
}
