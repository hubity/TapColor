using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class GameManager : MonoBehaviour
{
    public GameObject gameoverPanel;
    public GameObject playePanel;
    public GameObject pauseMenuUI;
    public GameObject Btn_MuteTrue;
    public GameObject Btn_MuteFalse;
    public bool isPlay = false;

    float S, V, H;
    public Image img;

    public int isReset;

    public int score;

    public bool GameIsPaused = false;

    public int curretScore;

    public int ismute;

    void Awake()
    {
        Time.timeScale = 1f;
    }
     void Start()
    {
        score = PlayerPrefs.GetInt("score");
        ismute = PlayerPrefs.GetInt("ismute");
        ChangeColorBackgroundFaddedScreenPlay();
        isReset = PlayerPrefs.GetInt("isReset");
        StartCoroutine(rs_Reset());
        if(isReset == 1)
        {
            isPlay = true;
            playePanel.SetActive(false);
        }
        if (ismute == 1)
        {
            Btn_MuteTrue.SetActive(true);
            Btn_MuteFalse.SetActive(false);
        }
        else
        {
            Btn_MuteTrue.SetActive(false);
            Btn_MuteFalse.SetActive(true);
        }
    }

    public void Update ()
    {
        curretScore = PlayerPrefs.GetInt("BestScore");
    }
    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.5f);
        gameoverPanel.SetActive(true);
        GetComponent<ScoreManager>().ChangeColorToWhite();
        yield break;
    }

    IEnumerator rs_Reset()
    {
        yield return new WaitForSeconds(3);
        PlayerPrefs.SetInt("isReset", 0);
        Debug.Log("reset");
        yield break;
    }

    public void Reset() 
    {
        PlayerPrefs.SetInt("isReset", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Store()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void PlayGame()
    {
        isPlay = true;
        playePanel.SetActive(false);
    }

    public void ChangeColorBackgroundFaddedScreenPlay()
    {
        H = Random.Range(0, 1f);
        S = Random.Range(0, 1f);
        V = Random.Range(0, 1f);
        img.color = new Color (H, S, V, 0.9f);
    }

    public void Score(int n)
    {
        score = score + n;
        PlayerPrefs.SetInt("score", score);
    }

   public void GamePause()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ShowLeaderBoardUI()
    {
        PlayServices.ShowLeaderboard(GPGSIds.leaderboard_ranking);
    }

}
