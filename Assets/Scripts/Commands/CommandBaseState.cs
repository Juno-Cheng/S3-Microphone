using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CommandState
{
    // function that sets up the state
    public abstract void Setup(CommandManager command);

    // function that updates every frame
    public abstract void LoopUpdate(CommandManager command);

    // function that runs when the state ends
    public abstract void Conclude(CommandManager command);
}