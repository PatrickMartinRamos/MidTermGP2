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

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Renderer playerRenderer = player.GetComponent<Renderer>();

        playerMaterial = playerRenderer.material;
        GetComponent<Renderer>().material = playerMaterial;
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Material enemyMaterial = collision.gameObject.GetComponent<Renderer>().material;

            // Check if the bullet material color matches the enemy material color
            if (playerMaterial.color == enemyMaterial.color)
            {
                Debug.Log("Bullet and enemy have the same color. Destroying both.");
                Destroy(collision.gameObject); // Destroy the enemy
                Destroy(gameObject); // Destroy the bullet
                scoreManager.AddScore(2);            
            }
            else
            {
                Debug.Log("Bullet and enemy have different colors. Destroying the bullet only.");
                Destroy(gameObject); // Destroy the bullet only
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
