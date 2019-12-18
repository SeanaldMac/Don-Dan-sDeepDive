using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{

    // destroys objects that collide with this object
    void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);


    }




}
