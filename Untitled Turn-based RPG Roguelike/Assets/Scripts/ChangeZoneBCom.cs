using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZoneBCom : BattleCommand
{
    Battlezone start;
    Battlezone end;

    public ChangeZoneBCom(Combatant actor, Battlezone target) : base(actor)
    {
        start = combatant.battlezone;
        end = target;
    }

  

    public override void Execute()
    {
        base.Execute();
        combatant.MoveTo(end);
    }
}
