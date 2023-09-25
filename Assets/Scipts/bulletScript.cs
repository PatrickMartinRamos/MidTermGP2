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
        //if the bullet collide with an object tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
                Destroy(collision.gameObject);

                // Destroy the bullet
                Destroy(gameObject);

                //call the scoreManager script and call the function add score
                scoreManager.AddScore(2);         
             
        }
    }
    // when the bullet is not seen in the camera destroy it
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
