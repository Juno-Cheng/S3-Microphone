using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandLaserState : CommandState
{
    float laserSpeed = 0.3f;
    public void Setup(CommandManager command){
        GameObject spawned = command.spawnObject(command.laser);
        
    }
    public void LoopUpdate(CommandManager command){
        Transform spot = command.spawnSpot.GetComponentInChildren<Transform>();
        spot.transform.localPosition += new Vector3(0,0,laserSpeed/2) * Time.deltaTime;
        spot.transform.localScale += new Vector3(0,0,laserSpeed) * Time.deltaTime;

        
    }

    public void Conclude(CommandManager command){
        GameObject spot;
        while (spot = command.spawnSpot.GetChild(0).gameObject) {
            command.destroyObject(spot);
        }

    }
}
