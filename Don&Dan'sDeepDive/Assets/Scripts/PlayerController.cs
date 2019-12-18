using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed, immuneTime, shotDelay, sinkSpeed;
    public bool canFire = true, immune = false;
    public int score = 0;
    public string shootButton, hMoveButton, sinkButton;

    Rigidbody2D rb;
    public BoxCollider2D playArea;
    public GameObject shot;
    public Transform shotSpawn;
    public Text scoreText;
    public SpriteRenderer sr;

    public GameController gc;
   





    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        
    }

    
    void Update()
    {
        // player movement only vertically
        // float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis(hMoveButton);

        float moveV;
        if (Input.GetButton(sinkButton))
        {
            moveV = sinkSpeed;
        }
        else
            moveV = 0;

        Vector2 move = new Vector2(moveH, moveV);

        rb.velocity = move * speed;

        // set player bounds
        float clampX = Mathf.Clamp(rb.position.x, playArea.bounds.min.x, playArea.bounds.max.x);
        float clampY = Mathf.Clamp(rb.position.y, playArea.bounds.min.y, playArea.bounds.max.y);

        rb.position = new Vector2(clampX, clampY);

        // fire shot when Player1 'space' is pressed/held with a cooldown
        if(Input.GetButtonDown(shootButton) && canFire)
        {
            Shoot();
            StartCoroutine(ShotCooldown());
        }
        if (Input.GetButton(shootButton) && canFire)
        {
            Shoot();
            StartCoroutine(ShotCooldown());
        }





    }

    // spawns a shot
    void Shoot()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    // cooldown between shots
    IEnumerator ShotCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(shotDelay);
        canFire = true;
    }

    /*
    public void LoseLife()
    {
        gc.HitByEnemy(HPBar, hp);
        

        if (gc. <= 0)
        {
            if(gameObject.CompareTag("Player"))
                gc.p1Alive = false;
            else if(gameObject.CompareTag("Player2"))
                gc.p2Alive = false;

            gc.GameOver();
            Destroy(gameObject);
            
            
        }
        else
            StartCoroutine(Immunity());



    }
    */

    public void ImmuneStart()
    {
        StartCoroutine(Immunity());
    }

    IEnumerator Immunity()
    {
        immune = true;
        sr.color = Color.red;
        yield return new WaitForSeconds(immuneTime);
        immune = false;
        sr.color = Color.white;
        
    }

    







}
