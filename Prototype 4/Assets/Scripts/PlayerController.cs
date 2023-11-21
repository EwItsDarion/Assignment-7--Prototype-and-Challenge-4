/*Darion Jeffries
 * Prototype 4
 * Controls player
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    private GameObject focalPoint;
    private float forwardInput;
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    public Text text;
    private bool lost = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.FindGameObjectWithTag("FocalPoint");
        lost = false;
    }


    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5F, 0);
        if (lost && Input.GetKeyDown(KeyCode.R))
        {
            reloadScene();
        }
    }

    private void FixedUpdate()
    {
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        if (transform.position.y < -10)
        {
            //Destroy(gameObject); <- breaks scene reloading
            text.text = "You lose! Press R to restart!";
            lost = true;
        }

        
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup) 
        {
            Debug.Log("Player collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("The scene reloading is working");
    }
    
}
