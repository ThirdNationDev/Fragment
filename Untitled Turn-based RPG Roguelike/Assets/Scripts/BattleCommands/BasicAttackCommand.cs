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
    private readonly bool selfTarget = false;
    private Combatant actor;
    private float damageDealt;
    private IEquipableSkill skill;
    private ITargetable target;
    public Combatant Actor { get => actor; set => actor = value; }
    public bool EndsTurn { get => endsTurn; }
    public bool SelfTarget => selfTarget;
    public IEquipableSkill Skill { get => skill; set => skill = value; }
    public ITargetable Target { get => target; set => target = value; }

    public void Execute()
    {
        Assert.IsNotNull(Actor);
        Assert.IsNotNull(target);
        Assert.IsNotNull(skill);

        Combatant targetCombatant = target as Combatant;
        damageDealt = BattleCalculator.CalculateDamage(actor, targetCombatant, skill.SkillStats.SkillDamage());
        targetCombatant.ReceiveDamage(damageDealt);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}