using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsInitialized { get; set; }
    public int CurrentScore { get; set; }

    private const string HighScoreKey = "HighScore";
    private const string MainMenuScene = "MainMenu";
    private const string DotRescueScene = "DotRescue";

    public int HighScore
    {
        get => PlayerPrefs.GetInt(HighScoreKey, 0);
        set => PlayerPrefs.SetInt(HighScoreKey, value);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        CurrentScore = 0;
        IsInitialized = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    public void GoToDotRescue()
    {
        SceneManager.LoadScene(DotRescueScene);
    }
}