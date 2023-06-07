using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MoveCommand : CommandManager.ITargetZoneCommand
{
    private Combatant actor;
    private Battlezone endZone;
    private Battlezone startZone;

    public MoveCommand()
    {
        actor = null;
        endZone = null;
        startZone = null;
    }

    public void Execute()
    {
        Assert.IsNotNull(endZone);
        Assert.IsNotNull(startZone);
        Assert.IsNotNull(actor);

        startZone.RemoveCombatant(actor);
        endZone.AddCombatant(actor);
    }

    public void SetActor(Combatant actor)
    {
        this.actor = actor;
        this.startZone = actor.battlezone;
    }

    public void SetTarget(Battlezone target)
    {
        this.endZone = target;
    }

    public override string ToString()
    {
        return "Moved from " + startZone.ToString() + " to " + endZone.ToString();
    }

    public void Undo()
    {
    }
}