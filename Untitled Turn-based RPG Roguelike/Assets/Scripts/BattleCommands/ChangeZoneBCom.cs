using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeZoneBCom : BattleCommand
{


    //public override void Execute()
    //{
    //    base.Execute();
    //    combatant.MoveTo(end);
    //}
    public override void Execute()
    {
        if(zoneTarget == null) //do nothing if no valid zone target
        {
            return;
        }

        base.Execute();
        int distance = Mathf.Abs(combatant.battlezone.zoneNumber - zoneTarget.zoneNumber);
        if(distance <= combatant.stats.stepsRemaining)
        {
            combatant.battlezone.RemoveCombatant(combatant.gameObject);
            zoneTarget.AddCombatant(combatant);
            combatant.stats.stepsRemaining -= distance;
        }

    }

    public override void Initialize(Combatant actor)
    {
        base.Initialize(actor);
        endsTurn = false;
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
}
