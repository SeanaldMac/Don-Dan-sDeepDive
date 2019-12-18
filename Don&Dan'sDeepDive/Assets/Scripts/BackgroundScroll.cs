using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float startX, endX, speed;



    

   

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;

        // looping background
        if (pos.x < endX)
                pos.x = startX;

        transform.position = pos;
    }
}
