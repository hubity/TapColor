using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public float velocity = 1;
    float distance = 2;
    float angle = 0;

    Player playerScript;

    // Start is called before the first frame update
    void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.isDead) return;
        Move();
        
    }

    void Move()
    {
        transform.position = new Vector2(Mathf.Sin(angle) * distance, transform.position.y);
        angle += velocity / 100f;
    }

    public IEnumerator LandingEffect()
    {
        Vector2 originalPosition = transform.position;
        float yChangeValue = 1f;

        while (yChangeValue > 0)
        {
            yChangeValue -= 0.1f;
            yChangeValue = Mathf.Clamp(yChangeValue, 0, 1.2f);
            transform.position = new Vector2(transform.position.x, originalPosition.y - yChangeValue);
            yield return 0;
        }
        yield break;
    }
}
