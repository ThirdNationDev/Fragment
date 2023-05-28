using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommand 
{
    internal Combatant combatant;
    internal Combatant combatTarget;
    internal Battlezone zoneTarget;
    internal Battlezone zoneStart;

    public bool endsTurn { get; internal set; }


    public virtual void Initialize(Combatant actor)
    {
        combatant = actor;
        zoneStart = actor.battlezone;
        endsTurn = false;
    }

    public virtual void SetTarget(Combatant target)
    {
        combatTarget = target;
    }

    public virtual void SetTarget(Battlezone target)
    {
        zoneTarget = target;
    }

    public virtual void Execute()
    {
        BattleManager.Instance.commandList.Push(this.ShallowClone());

    }

    public virtual void Undo()
    {
        BattleManager.Instance.commandList.Pop();

    }

    public virtual BattleCommand ShallowClone()
    {
        return this.MemberwiseClone() as BattleCommand;
    }

}
