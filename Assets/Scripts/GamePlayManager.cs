using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private List<int> levelSpeed;
    [SerializeField] private List<int> levelMax;

    private bool hasGameFinished;
    private float score;
    private float scoreSpeed;
    private int currentLevel;

    private void Awake()
    {
        GameManager.Instance.IsInitialized = true;
        score = 0f;
        currentLevel = 0;
        scoreSpeed = levelSpeed[currentLevel];
        UpdateScoreText();
    }

    private void Update()
    {
        if (hasGameFinished)
            return;

        score += scoreSpeed * Time.deltaTime;
        UpdateScoreText();

        if (score > levelMax[Mathf.Clamp(currentLevel, 0, levelMax.Count - 1)])
        {
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, levelSpeed.Count - 1);
            scoreSpeed = levelSpeed[currentLevel];
        }
    }

    public void GameEnded()
    {
        hasGameFinished = true;
        GameManager.Instance.CurrentScore = (int)score;
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GoToMainMenu();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = ((int)score).ToString();
    }
}