using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendCommand : CommandManager.ITargetSelfCommand
{
    private readonly bool endsTurn = true;
    private Combatant actor;
    private Battlezone startingZone;
    public Combatant Actor { get => actor; set => actor = value; }

    public bool EndsTurn => endsTurn;

    public Battlezone StartingZone { get => startingZone; set => startingZone = value; }

    public void Execute()
    {
        //TODO: Combatant defense increase
        //TODO:Combatant defense animation
    }

    public override string ToString()
    {
        return "Defended.";
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}