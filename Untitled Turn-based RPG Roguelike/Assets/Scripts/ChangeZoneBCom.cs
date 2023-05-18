using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZoneBCom : BattleCommand
{
    Battlezone start;
    Battlezone end;
    Combatant combatant;

    public ChangeZoneBCom(Combatant combatant, Battlezone end)
    {
        this.combatant = combatant;
        this.start = combatant.battlezone;
        this.end = end;
    }

    public override void Execute()
    {
        base.Execute();
        combatant.MoveTo(end);
    }
}
