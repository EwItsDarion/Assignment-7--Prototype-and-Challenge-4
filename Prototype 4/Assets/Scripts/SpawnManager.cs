using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerupPrefab;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        spawnPowerup(1);
        text.text = "Wave: " + waveNumber;
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    private void spawnPowerup(int powerupsToSpawn)
    {
        for (int i = 0; i < powerupsToSpawn; i++)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (waveNumber == 2)
        {
            waveNumber = 0;
            text.text = "You win! Press R to Restart!";
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloadScene();
            }
        }
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
            if (waveNumber != 0)
            {
                {
                    waveNumber++;
                    SpawnEnemyWave(waveNumber);
                    spawnPowerup(1);
                    text.text = "Wave: " + waveNumber;
                }
            }
    }
    void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("The scene reloading is working");
    }
}
