using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCommand : CommandManager.ICommand
{
    public void Execute()
    {
        Debug.LogError("Empty Command Execute Called");
    }

    public void SetActor(Combatant actor)
    {
        Debug.LogError("Empty Command SetActor called with " + actor.ToString());
    }

    public override string ToString()
    {
        return "Empty Command.";
    }

    public void Undo()
    {
        Debug.LogError("Empty Command Undo Called");
    }
}