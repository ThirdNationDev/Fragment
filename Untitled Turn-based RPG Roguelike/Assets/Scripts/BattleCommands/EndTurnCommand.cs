using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EndTurnCommand : CommandManager.ITargetSelfCommand
{
    private Combatant actor;

    public void Execute()
    {
        BattleManager.Instance.StartTheNextCombatantTurn();
    }

    public void SetActor(Combatant actor)
    {
        this.actor = actor;
    }

    public override string ToString()
    {
        return "Turn ended.";
    }

    public void Undo()
    {
    }
}