using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommand 
{
    internal Combatant combatant;
    internal Combatant combatTarget;
    internal Battlezone zoneTarget;
    internal Battlezone zoneStart;
    internal string name;

    public bool endsTurn { get; internal set; }
    public bool targetsZone { get; internal set; }
    public bool targetsCombatant { get; internal set; }
    public bool targetsSelf { get; internal set; }

    [SerializeField]
    public int range;


    public virtual void Initialize(Combatant actor)
    {
        combatant = actor;
        zoneStart = actor.battlezone;
        endsTurn = false;
        name = "BattleCommand";

        //one of these must be set true in derived class
        targetsZone = false;
        targetsCombatant = false;
        targetsSelf = false;

    }

    internal virtual List<Combatant> getTargets()
    {
        List<Combatant> targets = new List<Combatant>();
        if (targetsSelf)
        {
            targets.Add(this.combatant);
        }
        else if (targetsCombatant)
        {
            targets = BattleManager.Instance.battlefield.getCombatants(
                zoneStart.zoneNumber - range, zoneStart.zoneNumber + range);
        }

        return targets;
    }

    public virtual void SetTarget(Combatant target)
    {
        if (targetsSelf) { target = combatant; }
        else { combatTarget = target; }
    }

    public virtual void SetTarget(Battlezone target)
    {
        zoneTarget = target;
    }

    public virtual void Execute()
    {
        Debug.Log("Base execute called for " + BattleManager.Instance.commandToExecute.ToString());
        BattleManager.Instance.commandList.Push(this);

    }

    public virtual void Undo()
    {
        BattleManager.Instance.commandList.Pop();

    }


}
