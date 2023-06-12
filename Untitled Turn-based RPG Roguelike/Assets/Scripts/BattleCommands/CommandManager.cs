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
    private readonly Stack<ICommand> commandsBuffer = new Stack<ICommand>();

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
            Assert.IsNotNull(commandsBuffer);
            return commandsBuffer.Count;
        }
    }

    public ICommand LastCommand
    {
        get
        {
            Assert.IsNotNull(commandsBuffer);
            ICommand command;
            if (commandsBuffer.TryPeek(out command))
            {
                return command;
            }
            return new EmptyCommand();
        }
    }

    #region UnityMethods

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

    #endregion UnityMethods

    #region ClassMethods

    public void AddAndExecuteCommand(ICommand command)
    {
        Assert.IsNotNull(command);

        executeCommand(command);

        commandsBuffer.Push(command);
    }

    public ICommand[] LastNCommands(int n)
    {
        Assert.IsTrue(n <= commandsBuffer.Count);
        ICommand[] array = commandsBuffer.ToArray();
        return array[(commandsBuffer.Count - n)..commandsBuffer.Count]; //last index not included in slice
    }

    internal ICommand CommandAtIndex(int n)
    {
        Assert.IsTrue(n < commandsBuffer.Count);
        ICommand[] array = commandsBuffer.ToArray();
        return array[n];
    }

    private void executeCommand(ICommand command)
    {
        command.Execute();
        if (command.EndsTurn)
        {
            BattleManager.Instance.StartTheNextCombatantTurn();
        }
    }

    #endregion ClassMethods

    #region TestMethods

    public void TestExecuteCommand(ICommand command)
    {
        executeCommand(command);
    }

    #endregion TestMethods
}