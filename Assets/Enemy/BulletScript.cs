using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Properties")]
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float speed = 50f;
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private MouseLook playerRef;

    private Rigidbody rb;

    void Start()
    {
        // Automatically destroy the projectile after its lifetime expires
        Destroy(gameObject, lifetime);

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Add velocity to the projectile
        rb.velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collider belongs to a laser beam
        if (collision.gameObject.CompareTag("Laser")) // Make sure the laser prefab is tagged "Laser" in the editor
        {
            Destroy(gameObject); // Destroy the enemy object
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy has detected the player!");
            // Access the PlayerScript component and call LoseLife
            MouseLook playerScript = collision.gameObject.GetComponent<MouseLook>();
            if (playerScript != null)
            {
                playerScript.LoseLife();
            }
            else
            {
                Debug.LogError("PlayerScript not found on the player GameObject!");
            }
        }

    }

}
