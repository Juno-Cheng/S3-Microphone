using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class FollowScript : MonoBehaviour
{
    [Header("Player Object")]
    [SerializeField] public NavMeshAgent enemy;
    [SerializeField] public int speed = 10;

    private Transform playerTransform;


    // Start is called before the first frame update
    private void Start()
    {
        // Find the player GameObject by its tag
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found in the scene. Make sure it's tagged 'Player'.");
        }

        // Ensure the NavMeshAgent is properly placed on the NavMesh
        if (!enemy.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not placed on a valid NavMesh surface.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemy.destination = playerTransform.position;
    }
}
