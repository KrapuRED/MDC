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
        {
            // Biarkan currentHealth memakai nilai yang sudah ada sekarang (tidak di-override)
            InitializeUI();
        }

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
            healthUIController.UpdateExtraHealthUI(currentExtraHealth);
        }
        else
        {
            currentHealth -= 1;

            if (currentHealth <= 0)
            {
                SoundEffectManager.Instance.PlaySoundEffect("Game Fail");

                GameManager.Instance.PlayerLosing();
            }

            healthUIController.HideExtraHeart();
            healthUIController.UpdateNormalHealthUI(currentHealth);
        }
    }
}
