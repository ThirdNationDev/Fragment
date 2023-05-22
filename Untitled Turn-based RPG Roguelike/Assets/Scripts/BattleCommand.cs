using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommand
{
    internal Combatant combatant;

    public BattleCommand(Combatant actor)
    {
        combatant = actor;
    }
    public virtual void Execute()
    {

    }

    public virtual void Undo()
    {

    }

}
