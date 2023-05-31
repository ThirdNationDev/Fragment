using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathBCom : BattleCommand
{
    public override void Execute()
    {
        base.Execute();
        combatant.gameObject.SetActive(false);
    }

    public override void Initialize(Combatant actor)
    {
        base.Initialize(actor);
        endsTurn = true;
        targetsSelf = true;
        SetTarget(actor);
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
        combatant.gameObject.SetActive(true);
    }

    public override string ToString()
    {
        return combatant.ToString() + " used " + this.GetType().Name + "<br>";
    }
}
