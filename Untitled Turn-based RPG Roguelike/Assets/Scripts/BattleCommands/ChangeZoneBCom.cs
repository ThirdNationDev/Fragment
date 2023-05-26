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
        base.Execute();
    }

    public override void Initialize(Combatant actor)
    {
        base.Initialize(actor);
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
