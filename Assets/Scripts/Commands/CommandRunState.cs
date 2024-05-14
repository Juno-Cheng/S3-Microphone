using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CommandRunState : CommandState
{
    private Vector3 velocity;
    private CharacterController controller;
    public float timeleft;
    // function that sets up the state
    public override void Setup(CommandManager command){
        timeleft = command.timePerRun;
        controller = command.GetComponent<CharacterController>();
    }

    // function that updates every frame
    public override void LoopUpdate(CommandManager command){
        timeleft -= Time.deltaTime;
        velocity = command.transform.forward * command.speed;
        controller.Move(velocity * Time.deltaTime);
        if (timeleft <= 0) {
            command.ChangeState(command.baseState);
        }
    }

    // function that runs when the state ends
    public override void Conclude(CommandManager command){

    }

    public void resetTimer(CommandManager command) {
        timeleft = command.timePerRun;
    }
}