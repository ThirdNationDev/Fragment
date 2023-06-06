using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCommand : CommandManager.ICommand
{
    public void Execute()
    {
        Debug.LogError("Empty Command Execute Called");
    }

    public void Undo()
    {
        Debug.LogError("Empty Command Undo Called");
    }

    public override string ToString()
    {
        return "Empty Command.";
    }
}
