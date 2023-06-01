using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeZoneBCom : BattleCommand
{

    public override void Execute()
    {
        if(zoneTarget == null || combatant.zonesInMoveRange == null) //do nothing if no valid zone target
        {
            return;
        }

        //if zone is in range, move there
        foreach(Battlezone zone in combatant.zonesInMoveRange)
        {
            if (zone.Equals(zoneTarget))
            {
                combatant.battlezone.RemoveCombatant(combatant);
                zoneTarget.AddCombatant(combatant);
                base.Execute();
            }
        }

    }

    public override void Initialize(Combatant actor)
    {
        base.Initialize(actor);
        endsTurn = false;
        targetsZone = true;
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
        return combatant.ToString() + " used " + this.GetType().Name
            + "to move from " + zoneStart.ToString() + " to " + zoneTarget.ToString()
            + ".<br>";
    }
}
