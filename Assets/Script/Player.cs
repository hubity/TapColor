using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    enum PlayerState
    {
        Standing, Jumping, Falling
    }

    public Transform playerParentTransform;
    PlayerState currentState = PlayerState.Falling;

    Rigidbody2D rb;
    BoxCollider2D bc2D;

    public float previousPosXofParent;

    public bool isDead = false;

    public GameObject shootEffectPrefab;
    public GameObject deadEffectPrefab;
    public GameObject collectedEffectPrefab;

    float hueValue;

    public Sprite[] playerSprite;
    public Sprite[] playerSprite2;
    public Sprite[] playerSprite3;
    public Sprite[] playerSprite4;
    public Sprite[] playerSprite5;
    public Sprite[] playerSprite6;
    SpriteRenderer spriteRenderer;

    public int personagem;
    public bool isGround;

    public GameManager my_GameManger;
    GameManager gameManager;

    public GameManager my_ScoreManger;
    ScoreManager scoreManager;
    public bool isPlay;

    public int x;
    public int score;

    public AdMob admob;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        hueValue = Random.Range(0,1f);
        ChangeBackgroundColor();

        gameManager = my_GameManger.GetComponent<GameManager>();
        scoreManager = my_ScoreManger.GetComponent<ScoreManager>();

        x = PlayerPrefs.GetInt("x");

        if (x == 0)
        {
            PlayerPrefs.SetInt("personagemEscolhido", 1);
            PlayerPrefs.SetInt("x", 2);
        }

    }
    void Update()
    {
        score = PlayerPrefs.GetInt("score");
        if (gameManager.GameIsPaused == false)
        {
            GetInput();
        }
        GetPreviousPositionOfParent();
        BounceWall();
        DeadCheck();

        personagem = PlayerPrefs.GetInt("personagemEscolhido");
        

        AnimacaoPersonagem();
        
        isPlay = gameManager.isPlay;
    }

    void GetInput()
    {
     
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlay)
            {
                if (currentState == PlayerState.Standing)
                {
                    Jump();
                }
                else if (currentState == PlayerState.Jumping)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
    }

    void GetPreviousPositionOfParent()
    {
        previousPosXofParent = transform.parent.transform.position.x;
    }

    void Jump()
    {
        FindObjectOfType<AudioManager>().Play("PlayerJump");
        bc2D.enabled = false;
        currentState = PlayerState.Jumping;
        rb.velocity = new Vector2(ParentVelocity(), 10);

        transform.SetParent(playerParentTransform);
        StartCoroutine(StairActive());
    }

    IEnumerator StairActive()
    {
        yield return new WaitForSeconds(0.5f);
        bc2D.enabled = true;
        yield break;
    }

    float ParentVelocity()
    {
        float parentSpeed = (transform.parent.transform.transform.position.x - previousPosXofParent) / Time.deltaTime;
        if(parentSpeed >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        return parentSpeed;
    }

    IEnumerator Shoot()
    {
        GameObject shootEffectObj = Instantiate(shootEffectPrefab, transform.position, Quaternion.identity);
        shootEffectObj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hueValue, 0.6f, 0.8f);
        Destroy(shootEffectObj, 1.0f);

        currentState = PlayerState.Falling;
        
        StopPlayer();

        yield return new WaitForSeconds(0.2f);

        ChangeBackgroundColor();

       
        rb.isKinematic = false;
        rb.velocity = new Vector2(0, -30);

        yield break;
    }

    //Detecta se o player vai sair da tela
    void BounceWall()
    {
        if(rb.position.x < -2.2f)
        {
            rb.position = new Vector2(-2.2f, rb.position.y);
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            spriteRenderer.flipX = false;

        }

        if (rb.position.x > 2.2f)
        {
            rb.position = new Vector2(2.2f, rb.position.y);
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            spriteRenderer.flipX = true;
        }
    }

    void DeadCheck()
    {
        if(isDead == false && Camera.main.transform.position.y - transform.position.y > 6)
        {
            StopPlayer();
            DeadEffect();
            isDead = true;
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
            if(score > PlayServices.GetPlayerScore(GPGSIds.leaderboard_ranking))
            {
            PlayServices.PostScore((long)score, GPGSIds.leaderboard_ranking);
            }
            AdMob.instance.deaths++;
            if (AdMob.instance.deaths >= 3)
            {
                AdMob.instance.deaths = 0;
                AdMob.instance.ShowPopUp();
            }
        }
    }

    public void StopPlayer()
    {
        rb.isKinematic = true;
        rb.velocity = new Vector2(0, 0);
        
    }
    
    void DeadEffect()
    {
        Destroy(Instantiate(deadEffectPrefab, transform.position, Quaternion.identity), 1.0f);
    }

    void CollectedFruitEffect()
    {
        Destroy(Instantiate(collectedEffectPrefab, transform.position, Quaternion.identity), 1.0f);
    }

    void ChangeBackgroundColor()
    {
        Camera.main.backgroundColor = Color.HSVToRGB(hueValue, 0.6f, 0.8f);
        hueValue += 0.8f;
        if(hueValue >= 1)
        {
            hueValue = 0;
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
            FindObjectOfType<AudioManager>().Play("PlayerStair");
            isGround = true;
            rb.velocity = Vector2.zero;

            currentState = PlayerState.Standing;

            transform.SetParent(other.gameObject.transform);
            GetPreviousPositionOfParent();
            StartCoroutine(other.gameObject.GetComponent<Stair>().LandingEffect());
            GameObject.Find("GameManager").GetComponent<ScoreManager>().AddScore();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
            isGround = false;
            GameObject.Find("Stairs").GetComponent<StairsManager>().MakeStair();
            Destroy(other.gameObject, 0.1f);
        } 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Fruit"))
        {
            scoreManager.AddScoreCollectFruit();
            gameManager.Score(1);
            Destroy(col.gameObject, 0.1f);
        }
        if (col.gameObject.CompareTag("Fruit2"))
        {
            scoreManager.AddScoreCollectFruit();
            gameManager.Score(3);
            Destroy(col.gameObject, 0.1f);
        }
    }

    public void AnimacaoPersonagem()
    {
        if(isGround == true)
        {
            if (personagem == 1)
            {
                spriteRenderer.sprite = playerSprite[0];
            }
            else if (personagem == 2)
            {
                spriteRenderer.sprite = playerSprite2[0];
            }
            else if (personagem == 3)
            {
                spriteRenderer.sprite = playerSprite3[0];
            }
            else if (personagem == 4)
            {
                spriteRenderer.sprite = playerSprite4[0];
            }
            else if (personagem == 5)
            {
                spriteRenderer.sprite = playerSprite5[0];
            }
            else if (personagem == 6)
            {
                spriteRenderer.sprite = playerSprite6[0];
            }
        }
        else if(isGround == false)
        {
            if (personagem == 1)
            {
                spriteRenderer.sprite = playerSprite[1];
            }
            else if (personagem == 2)
            {
                spriteRenderer.sprite = playerSprite2[1];
            }
            else if (personagem == 3)
            {
                spriteRenderer.sprite = playerSprite3[1];
            }
            else if (personagem == 4)
            {
                spriteRenderer.sprite = playerSprite4[1];
            }
            else if (personagem == 5)
            {
                spriteRenderer.sprite = playerSprite5[1];
            }
            else if (personagem == 6)
            {
                spriteRenderer.sprite = playerSprite6[1];
            }
        }
    }
  
}
