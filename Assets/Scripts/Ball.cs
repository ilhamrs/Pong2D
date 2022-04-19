using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public bool isBounce;
    public bool bonusGoal;
    public bool isLastHit1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int random = Random.Range(0, 2);
        Debug.Log(random);
        if (random == 0)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 6 || transform.position.y < -6)
        {
            GameManager.instance.SpawnBall();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.instance.BallBounceSfx();
        if (collision.gameObject.tag=="Racket Red" && !isBounce)
        {
            //Vector2 dir = new Vector2(1, 0).normalized;
            //rb.velocity = dir * speed;
            rb.velocity = rb.velocity.normalized * speed;
            StartCoroutine("DelayBounce");
            isLastHit1 = true;
        }

        if (collision.gameObject.tag == "Racket Blue" && !isBounce)
        {
            //Vector2 dir = new Vector2(-1, 0).normalized;
            //rb.velocity = dir * speed;
            rb.velocity = rb.velocity.normalized * speed;
            StartCoroutine("DelayBounce");
            isLastHit1 = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal 1")
        {
            SoundManager.instance.GoalSfx();
            GameManager.instance.player2Score++;
            if (bonusGoal)
            {
                GameManager.instance.player2Score++;
            }
            GameManager.instance.SpawnBall();
            Destroy(gameObject);
            if (GameManager.instance.goldenGoal)
            {
                GameManager.instance.GameOver();
            }
        }

        if (collision.gameObject.tag == "Goal 2")
        {
            SoundManager.instance.GoalSfx();
            GameManager.instance.player1Score++;
            if (bonusGoal)
            {
                GameManager.instance.player1Score++;
            }
            GameManager.instance.SpawnBall();
            Destroy(gameObject);
            if (GameManager.instance.goldenGoal)
            {
                GameManager.instance.GameOver();
            }
        }
    }

    private IEnumerator DelayBounce()
    {
        isBounce = true;
        yield return new WaitForSeconds(1f);
        isBounce = false;
    }
}
