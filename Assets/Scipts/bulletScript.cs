using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bulletScript : MonoBehaviour
{
    public float bulletSpeed;

    private scoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<scoreManager>();

        //Rigidbody rb = GetComponent<Rigidbody>();
        //Vector3 initialVelocity = transform.forward * bulletSpeed* Time.deltaTime;
        //rb.velocity = initialVelocity;
    }
    private void Update()
    {
        //move the bullet
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an object tagged "Enemy".
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the material of the enemy.
            Renderer enemyRenderer = collision.gameObject.GetComponent<Renderer>();
            if (enemyRenderer != null)
            {
                // Get the material of the bullet
                Renderer bulletRenderer = GetComponent<Renderer>();

                // Compare the colors of the materials.
                if (enemyRenderer.material.color == bulletRenderer.material.color)
                {
                    // Destroy both the enemy and the bullet.
                    Destroy(collision.gameObject);
                    Destroy(gameObject);

                    // Call the scoreManager script and call the function add score.
                    scoreManager.AddScore(2);
                }
                else
                {
                    // Destroy only the bullet.
                    Destroy(gameObject);
                }
            }
            else
            {
                // If the enemy doesn't have a Renderer component, destroy only the bullet.
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
