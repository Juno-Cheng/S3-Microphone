using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandState
{
    // function that sets up the state
    public virtual void Setup(CommandManager command){}

    // function that updates every frame
    public virtual void LoopUpdate(CommandManager command){}

    // function that runs when the state ends
    public virtual void Conclude(CommandManager command){}
}