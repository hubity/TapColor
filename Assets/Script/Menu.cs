using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] players;

    public int personagemEscolhido;

    float S, V, H;

     int n;

    public Text nameSelect;

    public int score;
    public Text scoreText;

    public int price;
    public Text priceText;

    public int currentIntCharacter;

    public Testes my_current;
    Testes current;

    public int currentInt;

    public GameObject PanelConfirm;
    public GameObject bntBuy, bntSelect;

    public bool[] statusPlayer;

 

    void Start()
    {
        players[currentIntCharacter].SetActive(true);
        ChangeBackgroundColor();
        n = PlayerPrefs.GetInt("personagem");
        PlayerNameSelect(n);
        current = my_current.GetComponent<Testes>();

        loadData();
        statusPlayer[0] = true;
    }

    public void Update()
    {
        score = PlayerPrefs.GetInt("score");
        currentIntCharacter = PlayerPrefs.GetInt("currentPage");
        scoreText.text = score.ToString();

        currentInt = current.n;

        SwitchCharacters();

        saveData();

    }

    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitPanelConfirm()
    {
        PanelConfirm.SetActive(false);
    }

    void ChangeBackgroundColor()
    {
        H = Random.Range(0, 1f);
        S = Random.Range(0, 1f);
        V = Random.Range(0, 1f);
        Camera.main.backgroundColor = new Color(H, S, V, 1f);
    }
    public void SwitchCharacters()
    {
        players[currentIntCharacter].SetActive(true);
        PlayerNameSelect(currentIntCharacter);
        if (currentInt == 0)
        {
            players[currentIntCharacter + 1].SetActive(false);
        }
        else if (currentInt == 1)
        {
            players[currentIntCharacter - 1].SetActive(false);
        }
        
    }
    public void PlayerNameSelect(int name)
    {
        if (name == 1)
        {
            nameSelect.text = "Ninja Frog";
            priceText.text = "100";
            if (statusPlayer[0] == true)
            {
                bntSelect.SetActive(true);
                bntBuy.SetActive(false);
            }
            else if (statusPlayer[0] == false)
            {
                bntSelect.SetActive(false);
                bntBuy.SetActive(true);
            }

        }
        if (name == 2)
        {
            nameSelect.text = "Mask Dude";
            priceText.text = "328";
            if (statusPlayer[1] == true)
            {
                bntSelect.SetActive(true);
                bntBuy.SetActive(false);
            }
            else if (statusPlayer[1] == false)
            {
                bntSelect.SetActive(false);
                bntBuy.SetActive(true);
            }
        }
        if (name == 3)
        {
            nameSelect.text = "Pink Man";
            priceText.text = "547";
            if (statusPlayer[2] == true)
            {
                bntSelect.SetActive(true);
                bntBuy.SetActive(false);
            }
            else if (statusPlayer[2] == false)
            {
                bntSelect.SetActive(false);
                bntBuy.SetActive(true);
            }
        }
        if (name == 4)
        {
            nameSelect.text = "Virtual Guy";
            priceText.text = "639";
            if (statusPlayer[3] == true)
            {
                bntSelect.SetActive(true);
                bntBuy.SetActive(false);
            }
            else if (statusPlayer[3] == false)
            {
                bntSelect.SetActive(false);
                bntBuy.SetActive(true);
            }
        }
        if (name == 5)
        {
            nameSelect.text = "Chameleon";
            priceText.text = "843";
            if (statusPlayer[4] == true)
            {
                bntSelect.SetActive(true);
                bntBuy.SetActive(false);
            }
            else if (statusPlayer[4]== false)
            {
                bntSelect.SetActive(false);
                bntBuy.SetActive(true);
            }
        }
        if (name == 6)
        {
            nameSelect.text = "AngryPig";
            priceText.text = "1972";
            if (statusPlayer[5] == true)
            {
                bntSelect.SetActive(true);
                bntBuy.SetActive(false);
            }
            else if (statusPlayer[5] == false)
            {
                bntSelect.SetActive(false);
                bntBuy.SetActive(true);
            }
        }
    }
    
    public void BuyPlayer(int scorePrice)
    {
        if(score >= scorePrice)
        {
            
            PanelConfirm.SetActive(true);

        }
        else if(score < scorePrice)
        {
            Debug.Log("saldo insulficiente");
        }
    }

    public void TScore(int valueScore)
    {
        int n = score;
        n = n - valueScore;
        Debug.Log(n);
        PlayerPrefs.SetInt("score", n);
    }
    public void TakeOutScore()
    {
        int n = currentIntCharacter;
        if (n == 2)
        {
           TScore(328);
        }
        if (n == 3)
        {
            TScore(547);
        }
        if (n == 4)
        {
            TScore(639);
        }
        if (n == 5)
        {
            TScore(843);
        }
        if (n == 6)
        {
            TScore(1973);
        }
    }
    public void SelectPlayer()
    {
        int n = currentIntCharacter;
        if (statusPlayer[n - 1] == true)
        {
            PlayerPrefs.SetInt("personagemEscolhido", n);
            SceneManager.LoadScene("Main");
        }
        else
        {
            Debug.Log("erro!!");
        }
    }
    public void Buy()
    {
        int n = currentIntCharacter;
        if (n == 2)
        {
            BuyPlayer(328);
        }
        if (n == 3)
        {
            BuyPlayer(547);
        }
        if (n == 4)
        {
            BuyPlayer(639);
        }
        if (n == 5)
        {
            BuyPlayer(843);
        }
        if (n == 6)
        {
            BuyPlayer(1973);
        }
    }
    void saveData()
    {
        PlayerPrefs.SetInt("P1", boolToInt(statusPlayer[0]));
        PlayerPrefs.SetInt("P2", boolToInt(statusPlayer[1]));
        PlayerPrefs.SetInt("P3", boolToInt(statusPlayer[2]));
        PlayerPrefs.SetInt("P4", boolToInt(statusPlayer[3]));
        PlayerPrefs.SetInt("P5", boolToInt(statusPlayer[4]));
        PlayerPrefs.SetInt("P6", boolToInt(statusPlayer[5]));

    }
    void loadData()
    {
        statusPlayer[0] = intToBool(PlayerPrefs.GetInt("P1", 0));
        statusPlayer[1] = intToBool(PlayerPrefs.GetInt("P2", 0));
        statusPlayer[2] = intToBool(PlayerPrefs.GetInt("P3", 0));
        statusPlayer[3] = intToBool(PlayerPrefs.GetInt("P4", 0));
        statusPlayer[4] = intToBool(PlayerPrefs.GetInt("P5", 0));
        statusPlayer[5] = intToBool(PlayerPrefs.GetInt("P6", 0));
    }
    public void ConfirmBuy()
    {
        int n = currentIntCharacter;
        PlayerPrefs.SetInt("personagemEscolhido", n);
        statusPlayer[currentIntCharacter - 1] = true;
        PanelConfirm.SetActive(false);
        TakeOutScore();
    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}

