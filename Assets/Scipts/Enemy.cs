using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public Material[] enemyColorMat;


    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        int randomMaterialIndex = Random.Range(0, enemyColorMat.Length);
        renderer.material = enemyColorMat[randomMaterialIndex];
    }

    // Update is called once per frame
    void Update()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        Transform player = playerObject.transform;

        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        Debug.DrawLine(transform.position, player.position, Color.red);
    }
}
