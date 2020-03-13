 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI best;
    public TextMeshProUGUI bestScoreScreenPlay;
    public TextMeshProUGUI scoreScreenGameOver;
    public TextMeshProUGUI scoreText;

    public int scoreInt;

    // Start is called before the first frame update
    void Start()
    {
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        bestScoreScreenPlay.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        currentScoreText.text = currentScore.ToString();
    }

    void Update()
    {
        scoreInt = PlayerPrefs.GetInt("score");
        scoreText.text = scoreInt.ToString();
    }

    public void AddScore()
    {
        currentScore++;
        currentScoreText.text = currentScore.ToString();
        scoreScreenGameOver.text = currentScore.ToString();

        if(currentScore > PlayerPrefs.GetInt("BestScore", 0))
        {
            bestScoreText.text = currentScore.ToString();
            PlayerPrefs.SetInt("BestScore", currentScore);
        }
    }

    public void AddScoreCollectFruit()
    {
        currentScore = currentScore + 1;
    }

    public void ChangeColorToWhite()
    {
        bestScoreText.color = Color.white;
        currentScoreText.color = Color.white;
        best.color = Color.white;
    }
}
