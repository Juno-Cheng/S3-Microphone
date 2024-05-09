using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootScript : MonoBehaviour
{
    [Header("Player Object")]
    [SerializeField] private Transform obj;

    [Header("Enemy Settings")]
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private int speed = 10;
    [SerializeField] private float minRange = 10f;
    [SerializeField] private float maxRange = 20f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float projectileSpeed = 20f;

    private float nextFireTime;

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, obj.position);

        // Check if within desired range
        if (distanceToPlayer < minRange)
        {
            // Move away from the player
            Vector3 moveDirection = transform.position - obj.position;
            Vector3 newPos = transform.position + moveDirection.normalized * enemy.speed * Time.deltaTime;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(newPos, out hit, 2f, NavMesh.AllAreas))
            {
                enemy.SetDestination(hit.position);
            }
        }
        else if (distanceToPlayer > maxRange)
        {
            // Move closer to the player
            enemy.SetDestination(obj.position);
        }
        else
        {
            // Stay within range and face the player
            enemy.ResetPath();
            FacePlayer();

            // Shoot projectiles at intervals
            if (Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (obj.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * enemy.angularSpeed);
    }

    void ShootProjectile()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Add force to the projectile to move it forward
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = projectileSpawnPoint.forward * projectileSpeed;

        // Optionally, destroy the projectile after a few seconds to prevent clutter
        Destroy(projectile, 5f);
    }

}
