using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CommandManager : MonoBehaviour
{
    
    public CommandState baseState = new CommandState();
    public CommandState laserState = new CommandLaserState();
    public CommandState bubbleState = new CommandBubbleState();
    public CommandRunState runState = new CommandRunState();

    [Header("Current State")]
    public CommandState currentState;

    [Header("Projectile/Object spawning point")]
    public Transform spawnSpot;

    [Header("Projectiles/Object prefabs")]
    public GameObject laser;
    public GameObject bubble;

    [Header("Movement settings")]
    public float speed = 6f;
    public float timePerRun = 0.25f;

    public float shieldTime = 0.50f;

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
            } else if (currentState == bubbleState || currentState == runState){
                ChangeState(baseState);
            }
        } else if (Input.GetMouseButtonDown(1)) {
            if (currentState == runState){
                runState.resetTimer(this);
            } else {
                ChangeState(runState);
            }
        }
    }

    // changes the current state and runs it's setup function
    public void ChangeState(CommandState newState) {
        if (currentState == newState) {
            return;
        }
        Debug.Log("Changing state");
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
