using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    [Header("Player Object")]
    [SerializeField] public Transform obj;
    [SerializeField] public int speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, obj.position, speed * Time.deltaTime);
    }
}
