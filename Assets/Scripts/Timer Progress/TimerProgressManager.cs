using TMPro;
using UnityEngine;

public class TimerProgressManager : MonoBehaviour
{
    public TMP_Text yearText;
    public GameObject timerUI;
    public GameObject continueButton;

    [SerializeField] private int startYear;
    [SerializeField] private int currentYear;
    [SerializeField] private int endYear;
    [SerializeField] private float timerYearUp;

    private float currentTimer;

    private void Start()
    {
        currentYear = startYear;
        yearText.text = $"Year {currentYear}";
    }

    private void Update()
    {
        if (currentYear >= endYear)
        {
            continueButton.SetActive(true);
            return;
        }

        if (!ScenarioManager.Instance.IsDoneRead)
            return;

        showTimer();

        if (currentTimer < timerYearUp)
        {
            currentTimer += Time.deltaTime;
        }
        else
        {
            currentYear++;
            yearText.text = $"Year {currentYear}";

            currentTimer = 0;
        }
    }

    public void showTimer()
    {
        timerUI.SetActive(true);
    }

    public void ContinueGameLevel()
    {
        GameManager.Instance.PlayerWinning();
    }
}
