using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerObj;
    
    float smooth = 0.3f;
    Vector2 velocity = Vector2.zero;
    int yOffset = 2;

    public GameObject gameoverPanel;
    // Update is called once per frame
    public void Update()
    {
        Follow();
        
    }

    public void Follow()
    {
        if (playerObj)
        {
            Vector3 targetPosition = new Vector3(0, playerObj.transform.position.y + yOffset, -10);

            if (targetPosition.y < transform.position.y) return;

            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smooth);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        else
        {
            StartCoroutine(GameOverCoroutine());
        }
        
    }

    IEnumerator GameOverCoroutine()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.5f);
        gameoverPanel.SetActive(true);
        yield break;
    }
}
