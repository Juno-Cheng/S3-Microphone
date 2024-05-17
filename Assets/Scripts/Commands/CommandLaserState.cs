using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandLaserState : CommandState
{
    float laserSpeed = 10f;
    float maxLength = 100f;
    GameObject spawned;
    public override void Setup(CommandManager command){
        spawned = command.spawnObject(command.laser);
        
    }
    public override void LoopUpdate(CommandManager command){
        if (spawned.transform.localScale.x > maxLength) return;
        spawned.transform.localPosition += new Vector3(0,0,laserSpeed/2) * Time.deltaTime;
        spawned.transform.localScale += new Vector3(0,0,laserSpeed) * Time.deltaTime;
    }

    public override void Conclude(CommandManager command){
        command.destroyObject(spawned);
    }
}
