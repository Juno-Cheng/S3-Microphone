using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class FollowScript : MonoBehaviour
{
    [Header("Player Object")]
    [SerializeField] public Transform obj;
    [SerializeField] public NavMeshAgent enemy;
    [SerializeField] public int speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.destination = obj.position;
    }
}
