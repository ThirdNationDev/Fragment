using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefendBCom : BattleCommand
{
    public override void Execute()
    {
        base.Execute();
    }

    public override void Initialize(Combatant actor)
    {
        base.Initialize(actor);
        endsTurn = true;
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
        return combatant.ToString() + " used " + this.GetType().Name + "<br>";
    }
}
