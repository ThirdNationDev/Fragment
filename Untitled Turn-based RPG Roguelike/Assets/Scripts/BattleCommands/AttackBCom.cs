using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBCom : BattleCommand
{
    float skillDamage = 10;
    float damageReceived;

    public override void Execute()
    {
        base.Execute();
        float damageDealt = skillDamage + combatant.stats.aggression;
       // damageReceived = BattleManager.Instance.CalculateDamage(damageDealt, combatTarget);
        //Excecute attack animation
        combatTarget.ReceiveDamage(damageReceived);

    }

    public override void Initialize(Combatant actor)
    {
        base.Initialize(actor);
        endsTurn = false;
        range = 2;
        name = "AttackBCom";
        targetsCombatant = true;
        damageReceived = 0;
    }

    public override void SetTarget(Combatant target)
    {
        base.SetTarget(target);
    }

    public override void SetTarget(Battlezone target)
    {
        base.SetTarget(target);
    }

    public override void Undo()
    {
        base.Undo();
    }

    public override string ToString()
    {
        return combatant.ToString() +" attacks " + combatTarget.ToString() + " for " 
            + damageReceived + " points of damage.";
    }
}
