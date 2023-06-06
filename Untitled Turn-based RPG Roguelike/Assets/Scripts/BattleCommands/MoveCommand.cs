using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MoveCommand : CommandManager.ICommand
{
    private Combatant combatant;
    private Battlezone endZone;
    private Battlezone startZone;

    public MoveCommand()
    {
        combatant = null;
        endZone = null;
        startZone = null;
    }

    public MoveCommand(Combatant actor, Battlezone start, Battlezone end)
    {
        Initialize(actor, start, end);
    }

    public void Execute()
    {
        Assert.IsNotNull(endZone);
        Assert.IsNotNull(startZone);
        Assert.IsNotNull(combatant);

        startZone.RemoveCombatant(combatant);
        endZone.AddCombatant(combatant);
    }

    public void Initialize(Combatant actor, Battlezone start, Battlezone end)
    {
        combatant = actor;
        startZone = start;
        endZone = end;
    }

    public override string ToString()
    {
        return "Moved from " + startZone.ToString() + " to " + endZone.ToString();
    }

    public void Undo()
    {
    }
}