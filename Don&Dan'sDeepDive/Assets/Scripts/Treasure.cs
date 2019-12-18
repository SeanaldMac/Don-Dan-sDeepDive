using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public int points;

    Rigidbody2D rb;
    public Vector2 direction;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction.y = (0);
        rb.velocity = direction;

       
    }





}
