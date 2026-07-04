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

    public void PlayerWinning()
    {
        Debug.Log("You Winning!");
        level++;
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
        level = 0; 

        //change scene to MainMenu 0
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
