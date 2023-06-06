using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendCommand : CommandManager.ICommand
{
    Combatant combatant;

    public DefendCommand(Combatant actor)
    {
        combatant = actor;
    }

    public void Execute()
    {
        //TODO: Combatant defense increase
        //TODO:Combatant defense animation

        //End the turn
        CommandManager.Instance.AddCommand(new EndTurnCommand());
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    public override string ToString()
    {
        return "Defended.";
    }


}
