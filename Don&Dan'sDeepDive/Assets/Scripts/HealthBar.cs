using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;


    private void Start()
    {
        bar = transform.Find("Bar");
        // bar.localScale = new Vector3(.4f, 1f);
    }

   public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }




}
