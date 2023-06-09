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
    private readonly bool endsTurn = false;
    private Combatant actor;
    private Battlezone startingZone;
    private ITargetable target;
    private Battlezone targetZone;

    public MoveCommand()
    {
        Actor = null;
        targetZone = null;
        startingZone = null;
    }

    public Combatant Actor { get => actor; set => actor = value; }
    public bool EndsTurn => endsTurn;
    public Battlezone StartingZone { get => startingZone; set => startingZone = value; }
    public ITargetable Target { get => target; set => target = value; }
    public Battlezone TargetZone { get => targetZone; set => targetZone = value; }

    public void Execute()
    {
        Assert.IsNotNull(targetZone);
        Assert.IsNotNull(Actor);

        StartingZone = actor.battlezone;
        StartingZone.RemoveCombatant(Actor);
        targetZone.AddCombatant(Actor);
    }

    public List<ITargetable> PotentialTargets()
    {
        throw new System.NotImplementedException();
    }

    public void SetTarget(ITargetable target)
    {
        throw new System.NotImplementedException();
    }

    public override string ToString()
    {
        return "Moved from " + StartingZone.ToString() + " to " + targetZone.ToString();
    }

    public void Undo()
    {
    }
}