using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ResurrectCommand : CommandManager.ICommand
{
    private readonly CommandManager.TargetType commandTargets = CommandManager.TargetType.TargetInactiveCombatants;
    private readonly bool endsTurn = true;
    private Combatant actor;
    private float damageDealt;
    private IEquipableSkill skill;
    private ITargetable target;
    public Combatant Actor { get => actor; set => actor = value; }
    public CommandManager.TargetType CommandTargets => commandTargets;
    public bool EndsTurn { get => endsTurn; }
    public IEquipableSkill Skill { get => skill; set => skill = value; }
    public ITargetable Target { get => target; set => target = value; }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}