using System;
using System.Collections;
using System.Collections.Generic;
using Utils;

using UnityEngine;
using UnityEngine.Assertions;

public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> CommandsBuffer = new Stack<ICommand>();

    public interface ICommand
    {
        public Combatant Actor
        {
            get;
            set;
        }

        public bool EndsTurn
        {
            get;
        }

        public Battlezone StartingZone
        {
            set;
            get;
        }

        void Execute();

        String ToString();

        void Undo();
    }

    public interface ITargetCombatantCommand : ICommand
    {
        public Combatant TargetCombatant
        {
            get;
            set;
        }
    }

    public interface ITargetSelfCommand : ICommand
    {
    }

    public interface ITargetZoneCommand : ICommand
    {
        public Battlezone TargetZone
        {
            get;
            set;
        }
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
        CommandsBuffer.Push(command);
        command.Execute();
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