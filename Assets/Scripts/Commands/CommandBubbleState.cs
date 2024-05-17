using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandBubbleState : CommandState
{
    float bubbleSpeed = 3f;
    GameObject spawned;
    public override void Setup(CommandManager command){
        spawned = command.spawnObject(command.bubble);
        
    }
    public override void LoopUpdate(CommandManager command){
        spawned.transform.localScale += Vector3.one * bubbleSpeed * Time.deltaTime;
    }

    public override void Conclude(CommandManager command){
        command.destroyObject(spawned);

    }
}
