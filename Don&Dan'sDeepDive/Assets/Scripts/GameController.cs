using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] enemyPrefabs, treasurePrefabs;
    public GameObject gameOverPanel, oTankPrefab;
    public Text score1Text, score2Text, winnerText;
    public int score1, score2, enemyStartDelay, treasureStartDelay, oTankStartDelay;
    public bool p1Alive = true, p2Alive = true;
    public HealthBar healthBar1, healthBar2;
    public float health1, health2;
    public AudioSource gameplayAudio;



    void Start()
    {
        Cursor.visible = false;

        gameplayAudio = GetComponent<AudioSource>();
        gameplayAudio.Play();

        StartCoroutine(SpawnWave(enemyStartDelay, 3, new float[] { -2, 2, 0 }, new int[] { 0, 1, 0 }));
        // StartCoroutine(SpawnWave(8, 6, new float[] { 0, 3, 5, 2, 1, -3 }, new int[] { 0, 1, 1, 1, 0, 1 }));

        StartCoroutine(SpawnTreasure(treasureStartDelay));
        StartCoroutine(SpawnOxygenTanks(oTankStartDelay));

        health1 = 1f;
        health2 = 1f;

        // DecremateHealth(healthBar1, health1);
        StartCoroutine(DecremateHealth1());
        StartCoroutine(DecremateHealth2());
    }

    private void Update()
    {
        //GameOver();

        if(Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene("Level1");
        }

        else if(Input.GetButtonDown("Cancel"))
            SceneManager.LoadScene(0);
    }

    // spawn a wave of enemies
    IEnumerator SpawnWave(float startDelay, int numEnemies, float[] spawnPositions, int[] enemyTypes)
    {
        yield return new WaitForSeconds(startDelay);
        while (!gameOverPanel.activeInHierarchy)
        {
            for (int i = 0; i < numEnemies && i < spawnPositions.Length && i < enemyTypes.Length; i++)
            {
                Vector2 spawnPos;

                spawnPos = new Vector2(11, spawnPositions[i]);
                Instantiate(enemyPrefabs[enemyTypes[i]], spawnPos, Quaternion.identity);
            }

            // set new wave of enemies of random quantity with random spawn locations and of random type
            numEnemies = Random.Range(2, 10);
            spawnPositions = new float[numEnemies];
            enemyTypes = new int[numEnemies];
            for(int i = 0; i<numEnemies; i++)
            {
                spawnPositions[i] = Random.Range(-3.5f, 3.0f);
                enemyTypes[i] = Random.Range(0, enemyPrefabs.Length);
            }

            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        }


    }

    IEnumerator SpawnTreasure(float treasureStartDelay)
    {
        yield return new WaitForSeconds(treasureStartDelay);
        while (!gameOverPanel.activeInHierarchy)
        {
            Instantiate(treasurePrefabs[Random.Range(0, treasurePrefabs.Length)], new Vector2(11, Random.Range(-2.5f, -4.5f)), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(1.0f, 4.0f));
        }
    }

    IEnumerator SpawnOxygenTanks(float oTankStartDelay)
    {
        yield return new WaitForSeconds(oTankStartDelay);
        while (!gameOverPanel.activeInHierarchy)
        {
            Instantiate(oTankPrefab, new Vector2(Random.Range(-5.0f, 4.5f), 5.3f), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0.8f, 2.0f));
        }
    }

    public void GameOver()
    {
        if(!p1Alive && !p2Alive)
        {
            gameplayAudio.Stop();
            // determine who the winner is based off of points
            if (score1 > score2)
                winnerText.text = "Player 1 WINS";
            else if(score2 > score1)
                winnerText.text = "Player 2 WINS";
            else
                winnerText.text = "It's a Tie";

            gameOverPanel.SetActive(true);
        }
    }

    public void AddToScore1(int points)
    {
        score1 += points;
        score1Text.text = "Score: " + score1.ToString();
    }

    public void AddToScore2(int points)
    {
        score2 += points;
        score2Text.text = "Score: " + score2.ToString();
    }

    IEnumerator DecremateHealth1()
    {
        // yield return new WaitForSeconds(1.05f);

        while (!gameOverPanel.activeInHierarchy && health1 != 0)
        {
            if (health1 <= 0)
            {
                health1 = 0;
                Destroy(GameObject.FindWithTag("Player"));
                p1Alive = false;
                GameOver();
            }
            if (health1 > 0)
            {
                health1 -= .01f;
                healthBar1.SetSize(health1);
            }

            yield return new WaitForSeconds(.15f);
        }
    }

    IEnumerator DecremateHealth2()
    {
        // yield return new WaitForSeconds(1.05f);

        while (!gameOverPanel.activeInHierarchy && health2 != 0)
        {
            if (health2 <= 0)
            {
                health2 = 0;
                Destroy(GameObject.FindWithTag("Player2"));
                p2Alive = false;
                GameOver();
            }
            if (health2 > 0)
            {
                health2 -= .01f;
                healthBar2.SetSize(health2);
            }

            yield return new WaitForSeconds(.15f);
        }
    }

    // decrease health if hit by an enemy
    public void HitByEnemy1()
    {
        health1 -= .1f;

        if (health1 < 0)
        {
            health1 = 0;
            Destroy(GameObject.FindWithTag("Player"));
            p1Alive = false;
            GameOver();
        }
        if (health1 > 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().ImmuneStart();
            healthBar1.SetSize(health1);
        }

    }

    public void HitByEnemy2()
    {
        health2 -= .1f;

        if (health2 < 0)
        {
            health2 = 0;
            Destroy(GameObject.FindWithTag("Player2"));
            p2Alive = false;
            GameOver();
        }
        if (health2 > 0)
        {
            GameObject.FindWithTag("Player2").GetComponent<PlayerController>().ImmuneStart();
            healthBar2.SetSize(health2);
        }

    }

    public void IncreaseHealth1()
    {
        if(health1 > 0)
        {
            if (health1 <= .7f)
                health1 += .3f;
            else
                health1 = 1f;
            healthBar1.SetSize(health1);
        }
    }

    public void IncreaseHealth2()
    {
        if (health2 > 0)
        {
            if (health2 <= .7f)
                health2 += .3f;
            else
                health2 = 1f;
            healthBar2.SetSize(health2);
        }
    }





}
