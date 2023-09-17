using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bulletScript : MonoBehaviour
{
    public float bulletSpeed;
    private Material playerMaterial;

    private scoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<scoreManager>();

        //Rigidbody rb = GetComponent<Rigidbody>();
        //Vector3 initialVelocity = transform.forward * bulletSpeed* Time.deltaTime;
        //rb.velocity = initialVelocity;

        //find the player with tag "Player" and get the its material
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Renderer playerRenderer = player.GetComponent<Renderer>();
        playerMaterial = playerRenderer.material;
        GetComponent<Renderer>().material = playerMaterial;

    }
    private void Update()
    {
        //move the bullet
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        //if the bullet collide with an object tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //get the material component of the enemy 
            Material enemyMaterial = collision.gameObject.GetComponent<Renderer>().material;

            // Check if the bullet material color matches the enemy material color
            if (playerMaterial.color == enemyMaterial.color)
            {
                Debug.Log("Bullet and enemy have the same color. Destroying both.");

                // Destroy the enemy
                Destroy(collision.gameObject);

                // Destroy the bullet
                Destroy(gameObject);

                //call the scoreManager script and call the function add score
                scoreManager.AddScore(2);         
            }
            //if the color of the bullet doesnt match destroy just the bullet
            else
            {
                Debug.Log("Bullet and enemy have different colors. Destroying the bullet only.");
                //Destroy the bullet only
                Destroy(gameObject);
            }
        }
    }
    // when the bullet is not seen in the camera destroy it
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
