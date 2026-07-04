using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [SerializeField] private int level;
    public int Level => level;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void OnEndGame()
    {
        
    }

    public void OnPlayGame()
    {
        if (level >= 7)
        {
            LevelManager.Instance.LoadScene($"Credit", "CrossFade");
            return;
        }
        else if (level >= 6)
        {
            Debug.Log("Reach the END");
            level++;
            LevelManager.Instance.LoadScene($"GamePlay-MainMenu-{level}", "CrossFade");
            return;
        }

        LevelManager.Instance.LoadScene($"GamePlay-Main-{level}", "CrossFade");
    }

    public void PlayerWinning()
    {
        Debug.Log("You Winning!");
        level++;

        if (level == 4)
        {
            LevelManager.Instance.LoadScene($"GamePlay-ChangeEra", "CrossFade");
        }
        else
            LevelManager.Instance.LoadScene($"GamePlay-MainMenu-{level}", "CrossFade");

    }

    public void PlayerLosing()
    {
        if (level >= 5)
        {
            Debug.Log("YOU GET LIABILITY TERMINATED");
           
        }
        else
            Debug.Log("You are FIRED!");

        GlobalEvent.OnShowDeathPanel.Invoke();
    }

    public void OnRetryGame()
    {
        level = 1;

        LevelManager.Instance.LoadScene($"GamePlay-MainMenu-{level}", "CrossFade");
        //change scene to MainMenu 0
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
