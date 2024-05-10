using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CommandManager : MonoBehaviour
{
    
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
        currentState = laserState;
        currentState.Setup(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null) {
            currentState.LoopUpdate(this);
        }
        if (Input.GetMouseButtonDown(0)) {
            //ChangeState(bubbleState);
        }
    }

    // changes the current state and runs it's setup function
    public void ChangeState(CommandState newState) {
        currentState.Conclude(this);
        currentState = newState;
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
