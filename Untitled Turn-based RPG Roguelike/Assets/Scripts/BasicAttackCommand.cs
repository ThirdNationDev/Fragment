using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;

public class BasicAttackCommand : CommandManager.ITargetCombatantCommand
{
    private Combatant actor;
    private Damage skillDamage;
    private Combatant target;

    public void Execute()
    {
        Assert.IsNotNull(actor);
        Assert.IsNotNull(target);
        Assert.IsNotNull(skillDamage);

        target.ReceiveDamage(skillDamage.ResultingDamageFromTo(actor, target));
    }

    public void SetActor(Combatant actor)
    {
        this.actor = actor;
    }

    public void SetTarget(Combatant target)
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}