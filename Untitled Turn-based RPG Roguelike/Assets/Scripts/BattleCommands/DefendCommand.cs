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

public class DefendCommand : CommandManager.ICommand
{
    private readonly bool endsTurn = true;
    private readonly bool selfTarget = true;
    private Combatant actor;
    private IEquipableSkill skill;
    private ITargetable target;
    public Combatant Actor { get => actor; set => actor = value; }
    public bool EndsTurn => endsTurn;
    public bool SelfTarget => selfTarget;
    public IEquipableSkill Skill { get => skill; set => skill = value; }
    public ITargetable Target { get => target; set => target = value; }

    public void Execute()
    {
        //TODO: Combatant defense increase
        //TODO:Combatant defense animation
    }

    public override string ToString()
    {
        return "Defended.";
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}