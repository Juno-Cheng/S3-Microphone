using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandBubbleState : CommandState
{
    float bubbleSpeed = 0.3f;
    public void Setup(CommandManager command){
        GameObject spawned = command.spawnObject(command.bubble);
        
    }
    public void LoopUpdate(CommandManager command){
        Transform spot = command.spawnSpot.GetComponentInChildren<Transform>();
        //spot.transform.localPosition += new Vector3(0,0,bubbleSpeed/2);
        spot.transform.localScale += Vector3.one * bubbleSpeed * Time.deltaTime;
        
    }

    public void Conclude(CommandManager command){
        Transform spot;
        while (spot = command.spawnSpot.GetChild(0)) {
            command.destroyObject(spot.gameObject);
        }

    }
}
