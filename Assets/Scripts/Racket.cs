using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    public string axis = "Vertical";
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (axis == "Vertical2" && GameData.instance.isSinglePlayer)
        {
            return;
        }

        //kontrol racket
        float v = Input.GetAxis(axis);
        rb.velocity = new Vector2(0, v) * speed;
        
        //batas atas
        if (transform.position.y > 1.5f)
        {
            transform.position = new Vector2(transform.position.x, 1.5f);
        }
        //batas bawah
        if (transform.position.y < -1.5f)
        {
            transform.position = new Vector2(transform.position.x, -1.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            anim.SetTrigger("Shoot");
        }
    }
}
