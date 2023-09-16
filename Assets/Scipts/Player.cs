using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.MessageBox;

public class Player : MonoBehaviour
{
    public float rangeValue;
    public Transform attackPoint;
    public GameObject bullet;

    public Material[] playerColorMat;
    private Renderer playerRenderer;

    public float fireRate;
    private float nextFireTime;

    public float rotationSpeed;
    private int currentMaterialIndex;

    private enemySpawner spawner;
    public GameObject gameOverScreen;

    private UIscript uiscript;

    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        spawner = FindObjectOfType<enemySpawner>();
        uiscript = FindObjectOfType<UIscript>();
    }

    // Update is called once per frame
    void Update()
    {
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

            spawner.StopSpawning();
            setGameOverScreen();

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
        uiscript.ShowUI();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeValue);
    }

}
