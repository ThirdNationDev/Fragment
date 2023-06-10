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

public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> CommandsBuffer = new Stack<ICommand>();

    public enum TargetType
    {
        TargetSelfOnly,
        TargetAllCombatants,
        TargetAllCombatantsExceptSelf,
        TargetAllZones,
        TargetAllZonesExceptCurrent
    }

    public interface ICommand
    {
        public Combatant Actor { get; set; }

        public TargetType CommandTargets { get; }
        public bool EndsTurn { get; }
        public IEquipableSkill Skill { get; set; }
        public ITargetable Target { get; set; }

        public void Execute();

        public string ToString();

        public void Undo();
    }

    public static CommandManager Instance { get; private set; }

    public int CommandCount
    {
        get
        {
            return CommandsBuffer.Count;
        }

        private set { }
    }

    public ICommand LastCommand
    {
        get
        {
            ICommand command = new EmptyCommand();
            CommandsBuffer.TryPeek(out command);
            return command;
        }

        private set { }
    }

    public void AddCommand(ICommand command)
    {
        command.Execute();
        CommandsBuffer.Push(command);

        if (command.EndsTurn)
        {
            BattleManager.Instance.StartTheNextCombatantTurn();
        }
    }

    public ICommand[] LastNCommands(int n)
    {
        Assert.IsTrue(n <= CommandsBuffer.Count);
        ICommand[] array = CommandsBuffer.ToArray();
        return array[(CommandsBuffer.Count - n)..CommandsBuffer.Count]; //last index not included in slice
    }

    internal ICommand CommandAtIndex(int n)
    {
        Assert.IsTrue(n < CommandsBuffer.Count);
        ICommand[] array = CommandsBuffer.ToArray();
        return array[n];
    }

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
}