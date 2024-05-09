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

    
}
