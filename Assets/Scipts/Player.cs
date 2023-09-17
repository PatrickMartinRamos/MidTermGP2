using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.MessageBox;

public class Player : MonoBehaviour
{
    public float rangeValue; //set the range value of the player 
    public Transform attackPoint; // this where the bullet will get instaciated
    public GameObject bullet; //bullet prefab

    public Material[] playerColorMat; //set the color material for the player
    private Renderer playerRenderer;

    public float fireRate;
    private float nextFireTime;

    public float rotationSpeed; //add rotation speed so player wont snap to the nearest enemy instead rotate it
    private int currentMaterialIndex; 

    private enemySpawner spawner; //get the enemySpawner Script
    public GameObject gameOverScreen; //get the gameOver screen 

    private UIscript uiscript;

    // Start is called before the first frame update
    void Start()
    {
        //get player renderer for the player
        playerRenderer = GetComponent<Renderer>(); 
        //find the object with script enemySpawener and UIscript
        spawner = FindObjectOfType<enemySpawner>();
        uiscript = FindObjectOfType<UIscript>();
    }

    // Update is called once per frame
    void Update()
    {
        //set the nearestEnemy to null
        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        // Check for enemies within rangeValue
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeValue);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                // Calculate the distance to the enemy
                float distanceToEnemy = Vector3.Distance(transform.position, col.transform.position);

                // If the current enemy is closer than the nearest one found so far, update the nearestEnemy var
                if (distanceToEnemy < nearestDistance)
                {
                    nearestEnemy = col.transform;
                    nearestDistance = distanceToEnemy;
                }
            }
        }
        // Check if a nearest enemy was found
        if (nearestEnemy != null)
        {
            // Get the position of the nearest enemy
            Vector3 enemyPosition = nearestEnemy.position;

            // Calculate the direction to the nearest enemy
            Vector3 lookDirection = enemyPosition - transform.position;

            // Calculate the rotation towards the nearest enemy
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            transform.DORotate(targetRotation.eulerAngles, rotationSpeed);
        }

        
        if (Time.time >= nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(bullet, attackPoint.position, attackPoint.rotation);
                // Set the next allowed fire time based on the fire rate
                nextFireTime = Time.time + fireRate;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Cycle through the materials in the array in order
            currentMaterialIndex = (currentMaterialIndex + 1) % playerColorMat.Length;
            Material nextMaterial = playerColorMat[currentMaterialIndex];

            // Assign the next material to the player's renderer
            playerRenderer.material = nextMaterial;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log("destroy");

            //if player collide with the player stop spawning
            spawner.StopSpawning();
            //showw the GameOverSCreen
            setGameOverScreen();

            //find and collect object with tag enemies 
            GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Destroy each remaining enemy
            foreach (GameObject enemy in remainingEnemies)
            {       
                Destroy(enemy);
            }
        }
    }

    public void setGameOverScreen()
    {
        //call Show UI function in UI script
        uiscript.ShowUI();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeValue);
    }

}
