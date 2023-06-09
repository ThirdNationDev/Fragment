/* Copyright (C) 2023 Thomas Payne, Third Nation Games - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the Third Nation Games license, which unfortunately won't be
 * written for another century.
 *
 * You should have received a copy of the Third Nation Games license with
 * this file. If not, please write to: dev@thirdnationgames.com, or visit : www.thirdnationgames.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BasicAttackCommand : CommandManager.ICommand
{
    private readonly bool endsTurn = true;
    private Combatant actor;
    private Damage skillDamage;
    private Battlezone startingZone;
    private ITargetable target;
    private Combatant targetCombatant;
    public Combatant Actor { get => actor; set => actor = value; }
    public bool EndsTurn { get => endsTurn; }
    public Battlezone StartingZone { get => startingZone; set => startingZone = value; }
    public ITargetable Target { get => target; set => target = value; }
    public Combatant TargetCombatant { get => targetCombatant; set => targetCombatant = value; }

    public void Execute()
    {
        Assert.IsNotNull(Actor);
        Assert.IsNotNull(targetCombatant);
        Assert.IsNotNull(skillDamage);

        targetCombatant.ReceiveDamage(skillDamage.ResultingDamageFromTo(Actor, targetCombatant));
    }

    public List<ITargetable> PotentialTargets()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}