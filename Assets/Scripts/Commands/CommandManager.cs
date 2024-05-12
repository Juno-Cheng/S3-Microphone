using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CommandManager : MonoBehaviour
{
    
    CommandState baseState = new CommandState();
    CommandState laserState = new CommandLaserState();
    CommandState bubbleState = new CommandBubbleState();

    [Header("Current State")]
    public CommandState currentState;

    [Header("Projectile/Object spawning point")]
    public Transform spawnSpot;

    [Header("Projectiles/Object prefabs")]
    public GameObject laser;
    public GameObject bubble;

    // Start is called before the first frame update
    void Start()
    {
        currentState = baseState;
        currentState.Setup(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null) {
            currentState.LoopUpdate(this);
        }
        if (Input.GetMouseButtonDown(0)) {
            if (currentState == baseState){
                ChangeState(laserState);
            } else if (currentState == laserState){
                ChangeState(bubbleState);
            } else if (currentState == bubbleState){
                ChangeState(baseState);
            }
        }
    }

    // changes the current state and runs it's setup function
    public void ChangeState(CommandState newState) {
        Debug.Log("pre-conclude");
        currentState.Conclude(this);
        Debug.Log("pre-swap");
        currentState = newState;
        Debug.Log("pre-setup");
        currentState.Setup(this);
    }

    // STATE FUNCTIONS (allows states to interact with monobehavior scene) 
    // spawns an object at a specified position and rotation
    public void spawnObject(GameObject obj, Vector3 pos, Quaternion rot, Transform parent) {
        Instantiate(obj,pos,rot,transform);
    }

    // spawns an object at the default location in front of the players face
    public GameObject spawnObject(GameObject obj) {
        return Instantiate(obj,spawnSpot);
    }

    // destroy an object

    public void destroyObject(GameObject obj) {
        Destroy(obj);
    }
}
