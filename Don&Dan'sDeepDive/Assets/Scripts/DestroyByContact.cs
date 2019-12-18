using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyByContact : MonoBehaviour
{
    public string tagToDestroy;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tagToDestroy))
        {

            /* add score
            if(this.CompareTag("Treasure") && tagToDestroy == "Player")
            {
                collision.GetComponent<PlayerController>().LoseLife();
            }
            */
            if (this.gameObject.CompareTag("Treasure") && tagToDestroy == "Player")
            {
                int points = this.GetComponent<Treasure>().points;
                Text scoreT = GameObject.FindWithTag("Player").GetComponent<PlayerController>().scoreText;
                GameObject.FindWithTag("GameController").GetComponent<GameController>().AddToScore1(this.GetComponent<Treasure>().points);
            }
            else if (this.gameObject.CompareTag("Treasure") && tagToDestroy == "Player2")
            {
                int points = this.GetComponent<Treasure>().points;
                Text scoreT = GameObject.FindWithTag("Player2").GetComponent<PlayerController>().scoreText;
                GameObject.FindWithTag("GameController").GetComponent<GameController>().AddToScore2(this.GetComponent<Treasure>().points);
            }
            else if (tagToDestroy == "Player" && !collision.GetComponent<PlayerController>().immune && !this.gameObject.CompareTag("OTank"))
            {
                GameObject.FindWithTag("GameController").GetComponent<GameController>().HitByEnemy1();
            }
            else if (tagToDestroy == "Player2" && !collision.GetComponent<PlayerController>().immune && !this.gameObject.CompareTag("OTank"))
            {
                GameObject.FindWithTag("GameController").GetComponent<GameController>().HitByEnemy2();
            }
            else if(tagToDestroy == "Enemy" || tagToDestroy == "Treasure")
            {
                Destroy(collision.gameObject);
            }
            else if(this.gameObject.CompareTag("OTank") && tagToDestroy == "Player")
            {
                GameObject.FindWithTag("GameController").GetComponent<GameController>().IncreaseHealth1();
            }
            else if (this.gameObject.CompareTag("OTank") && tagToDestroy == "Player2")
            {
                GameObject.FindWithTag("GameController").GetComponent<GameController>().IncreaseHealth2();
            }

            if(tagToDestroy == "BigEnemy")
            {
                if(!collision.GetComponent<Enemy>().immune)
                    collision.GetComponent<Enemy>().LoseALife();
            }
            if(!this.gameObject.CompareTag("BigEnemy"))
                Destroy(this.gameObject);

        }
    }






}
