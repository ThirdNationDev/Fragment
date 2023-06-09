using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;

public class BasicAttackCommand : CommandManager.ITargetCombatantCommand
{
    private readonly bool endsTurn = true;
    private Combatant actor;
    private Damage skillDamage;
    private Battlezone startingZone;
    private Combatant targetCombatant;
    public Combatant Actor { get => actor; set => actor = value; }
    public bool EndsTurn { get => endsTurn; }
    public Battlezone StartingZone { get => startingZone; set => startingZone = value; }
    public Combatant TargetCombatant { get => targetCombatant; set => targetCombatant = value; }

    public void Execute()
    {
        Assert.IsNotNull(Actor);
        Assert.IsNotNull(targetCombatant);
        Assert.IsNotNull(skillDamage);

        targetCombatant.ReceiveDamage(skillDamage.ResultingDamageFromTo(Actor, targetCombatant));
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}