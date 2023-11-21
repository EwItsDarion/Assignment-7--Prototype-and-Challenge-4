/*Darion Jeffries
 * Challenge 4
 * Controls enemy movement
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyX : MonoBehaviour
{
    public float speed;
    public int enemyBeat;
    public int total;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    public Text text;
    public bool lose = false;

    public bool GetLose()
    {
        return lose;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.FindGameObjectWithTag("Playergoal");
        if (playerGoal == null )
        {
            Debug.Log("Playergoal object was not found!");
        }
        enemyBeat = 0;

        SpawnManagerX spawnManager = FindObjectOfType<SpawnManagerX>();
        if (spawnManager != null) {
            speed = spawnManager.GetEnemySpeed();
            total = spawnManager.GetEnemyTotal();
            print("Enemy total: " + total);
            print("Enemy beat: " + enemyBeat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            enemyBeat++;
            if (enemyBeat == total)
            {
                lose = true;
                print(lose);
                text.text = "You Lose! Press R to Restart!";
                if (Input.GetKeyDown(KeyCode.R))
                {
                    reloadScene();
                }

            }
            Destroy(gameObject);
        }

    }
    public void reloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
