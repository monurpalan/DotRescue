using System.Collections;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text newBestText;
    [SerializeField] private float animationTime = 2f;
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private AudioClip clickSound;

    private void Awake()
    {
        highScoreText.text = GameManager.Instance.HighScore.ToString();

        if (GameManager.Instance.IsInitialized)
        {
            StartCoroutine(ShowScore());
        }
        else
        {
            scoreText.gameObject.SetActive(false);
            newBestText.gameObject.SetActive(false);
        }
    }

    private IEnumerator ShowScore()
    {
        int tempScore = 0;
        scoreText.text = tempScore.ToString();

        int currentScore = GameManager.Instance.CurrentScore;
        int highScore = GameManager.Instance.HighScore;

        if (currentScore > highScore)
        {
            newBestText.gameObject.SetActive(true);
            GameManager.Instance.HighScore = currentScore;
        }
        else
        {
            newBestText.gameObject.SetActive(false);
        }

        highScoreText.text = GameManager.Instance.HighScore.ToString();

        float speed = 1f / animationTime;
        float timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            tempScore = (int)(speedCurve.Evaluate(timeElapsed) * currentScore);
            scoreText.text = tempScore.ToString();
            yield return null;
        }

        scoreText.text = currentScore.ToString();
    }

    public void ClickedPlay()
    {
        SoundManager.Instance.PlaySound(clickSound);
        GameManager.Instance.GoToDotRescue();
    }
}