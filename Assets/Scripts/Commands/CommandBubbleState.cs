using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandBubbleState : CommandState
{
    float bubbleSpeed = 100f;
    GameObject spawned;
    public float timeleft;
    public override void Setup(CommandManager command){
        timeleft = command.shieldTime;
        spawned = command.spawnObject(command.bubble);
        
    }
    public override void LoopUpdate(CommandManager command){
        spawned.transform.localScale += Vector3.one * bubbleSpeed * Time.deltaTime;
        timeleft -= Time.deltaTime;
        if (timeleft <= 0) {
            command.ChangeState(command.baseState);
        }
    }

    public override void Conclude(CommandManager command){
        command.destroyObject(spawned);
    }
}
