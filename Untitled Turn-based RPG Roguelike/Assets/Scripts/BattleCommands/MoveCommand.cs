using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MoveCommand : CommandManager.ITargetZoneCommand
{
    private readonly bool endsTurn = false;
    private Combatant actor;
    private Battlezone startingZone;
    private Battlezone targetZone;

    public MoveCommand()
    {
        Actor = null;
        targetZone = null;
        startingZone = null;
    }

    public Combatant Actor { get => actor; set => actor = value; }

    public bool EndsTurn => endsTurn;

    public Battlezone StartingZone { get => startingZone; set => startingZone = value; }
    public Battlezone TargetZone { get => targetZone; set => targetZone = value; }

    public void Execute()
    {
        Assert.IsNotNull(targetZone);
        Assert.IsNotNull(Actor);

        StartingZone = actor.battlezone;
        StartingZone.RemoveCombatant(Actor);
        targetZone.AddCombatant(Actor);
    }

    public override string ToString()
    {
        return "Moved from " + StartingZone.ToString() + " to " + targetZone.ToString();
    }

    public void Undo()
    {
    }
}