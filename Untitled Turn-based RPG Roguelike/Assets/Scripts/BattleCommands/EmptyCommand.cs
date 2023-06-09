using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCommand : CommandManager.ICommand
{
    private readonly bool endsTurn = false;
    private Combatant actor;
    private Battlezone startingZone;
    public Combatant Actor { get => actor; set => actor = value; }

    public bool EndsTurn => endsTurn;

    public Battlezone StartingZone { get => startingZone; set => startingZone = value; }

    public void Execute()
    {
        Debug.LogError("Empty Command Execute Called");
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