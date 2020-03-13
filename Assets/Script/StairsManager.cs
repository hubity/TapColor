using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsManager : MonoBehaviour
{

    public GameObject stairPrefab;
    public GameObject stairPrefabFruit;

    int stairIndex = 0;
    int distanceToNextStair = 3;

   // float stairWidth = 3;
   // float stairHeight = 0.4f;

    float rangeV1;
    float rangeV2;

    ScoreManager score;
    public int scoreBest;

    public int setNewVelocity;

    public GameObject[] stair;
    int randomStair;
    // Start is called before the first frame update
    void Start()
    {
        //rangeV1 = 2f;
        //rangeV2 = 2.8f;
        rangeV1 = 1.5f;
        rangeV2 = 2f;
        setNewVelocity = 32;

    InitStairs();

        score = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        scoreBest = score.currentScore;
        NewVelocity();

    }

    void InitStairs()
    {
        for (int i = 0; i < 4; i++)
        {
            MakeStair();
        }
    }

    public void MakeStair()
    {
        randomStair = Random.Range(0, stair.Length);
        //Debug.Log(randomStair);
        Vector2 position = new Vector2(0, stairIndex * distanceToNextStair);
        
        
        GameObject newStairObj = Instantiate(stair[randomStair], position, Quaternion.identity);
        
        
        newStairObj.transform.SetParent(transform);
        //newStairObj.transform.localScale = new Vector2(stairWidth, stairHeight);

        SetSpeed(newStairObj);
        //DecreasesStairWidth();

        stairIndex ++;
    }

    void SetSpeed(GameObject newStairObj)
    {
        newStairObj.GetComponent<Stair>().velocity = Random.Range(rangeV1,rangeV2);
    }

    public void NewLevel()
    {
        if (scoreBest == 2)
        {

        }
    }

    void DecreasesStairWidth()
    {
        //if(stairWidth > 0.7f){
           // stairWidth -= 0.03f;
       // }
    }

    public void NewVelocity()
    {
        if (scoreBest == setNewVelocity)
        {
            setNewVelocity =  setNewVelocity * 2;
            rangeV1 = rangeV1 + 0.8f;
            rangeV2 = rangeV1 + 0.8f;

        }
    }
}
