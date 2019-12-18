using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D playArea;
    public Vector2 direction;
    public int lives;
    public float immuneTime;
    public bool immune = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction.y = Random.Range(-1.0f, 1.0f);
        rb.velocity = direction;

        playArea = GameObject.Find("PlayArea").GetComponent<BoxCollider2D>(); // use later for bouncing off playArea
    }

    private void Update()
    {
        if (rb.position.y > playArea.bounds.max.y && rb.velocity.y > 0)
        {
            direction.y = -Mathf.Abs(direction.y);
            rb.velocity = direction;
        }
        else if (rb.position.y < playArea.bounds.min.y && rb.velocity.y < 0)
        {
            direction.y = Mathf.Abs(direction.y);
            rb.velocity = direction;
        }
    }

    public void LoseALife()
    {
        lives--;
        if(lives <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(Immunity());
        }
    }

    IEnumerator Immunity()
    {
        immune = true;
        yield return new WaitForSeconds(immuneTime);
        immune = false;

    }




}
