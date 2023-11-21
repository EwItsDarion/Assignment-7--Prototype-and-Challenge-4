/*Darion Jeffries
 * Challenge 4
 * Manages spawn and win conditions
 * 
 */
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z
    private float speedIncrease = 2.0f;
    private float enemySpeed = 7.0f;
    public Text waveText;
    public bool win = false;
    public bool lose;

    public int enemyCount;
    public int waveCount = 1;


    public GameObject player; 

    // Update is called once per frame


    public void updateText()
    {
        waveText.text = "Wave: " + waveCount;
    }
    void Update()
    {
        EnemyX enemyX = GetComponent<EnemyX>();
        if (enemyX != null)
        {
            lose = enemyX.GetLose();
        }

        if (waveCount == 10 && enemyCount == 0)
        {
            print(win);
            win = true;
            waveText.text = "You win! Press R to Restart!";
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloadScene();
            }
        }
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        updateText();

        if (enemyCount == 0 && !lose && !win)
        {
            waveCount++;
            int enemiesToSpawn = waveCount;
            SpawnEnemyWave(enemiesToSpawn);
        }

    }

    public int GetEnemyTotal()
    {
        return waveCount;
    }
    public float GetEnemySpeed()
    {
        return enemySpeed;
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end
        enemySpeed *= speedIncrease;

        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
        ResetPlayerPosition(); // put player back at start

    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

    public void reloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
