using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendCommand : CommandManager.ITargetSelfCommand
{
    private Combatant actor;

    public void Execute()
    {
        //TODO: Combatant defense increase
        //TODO:Combatant defense animation

        //End the turn
        CommandManager.Instance.AddCommand(new EndTurnCommand());
    }

    public void SetActor(Combatant actor)
    {
        this.actor = actor;
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