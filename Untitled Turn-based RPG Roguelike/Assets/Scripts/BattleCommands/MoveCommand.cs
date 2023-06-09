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

public class MoveCommand : CommandManager.ICommand
{
    private readonly CommandManager.TargetType commandTargets = CommandManager.TargetType.TargetSelfOnly;
    private readonly bool endsTurn = false;
    private Combatant actor;
    private IEquipableSkill skill;
    private Battlezone startingZone;
    private ITargetable target;
    private Battlezone targetZone;

    public Combatant Actor { get => actor; set => actor = value; }
    public CommandManager.TargetType CommandTargets => commandTargets;
    public bool EndsTurn => endsTurn;
    public IEquipableSkill Skill { get => skill; set => skill = value; }
    public ITargetable Target { get => target; set => target = value; }

    public void Execute()
    {
        Assert.IsNotNull(Actor);
        Assert.IsNotNull(Target);
        Assert.IsNotNull(Skill);
        Assert.IsTrue(Target is Battlezone);

        startingZone = Actor.battlezone;
        startingZone.RemoveCombatant(Actor);
        targetZone = Target as Battlezone;
        targetZone.AddCombatant(Actor);
    }

    public void SetTarget(ITargetable target)
    {
        Assert.IsNotNull(target);
        Assert.IsTrue(target is Battlezone);
        this.target = target;
    }

    public override string ToString()
    {
        Assert.IsNotNull(startingZone);
        Assert.IsNotNull(targetZone);

        return "Moved from " + startingZone.ToString() + " to " + targetZone.ToString();
    }

    public void Undo()
    {
    }
}