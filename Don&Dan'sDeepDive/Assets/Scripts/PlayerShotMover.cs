using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotMover : MonoBehaviour
{
    public Vector2 direction;
    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction;
    }


    

}
