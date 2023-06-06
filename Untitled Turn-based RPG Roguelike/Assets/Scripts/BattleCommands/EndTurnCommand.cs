using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EndTurnCommand : CommandManager.ICommand
{
    public void Execute()
    {
        BattleManager.Instance.StartTheNextCombatantTurn();
    }

    public void Undo()
    {
        
    }

    public override string ToString()
    {
        return "Turn ended.";
    }
}