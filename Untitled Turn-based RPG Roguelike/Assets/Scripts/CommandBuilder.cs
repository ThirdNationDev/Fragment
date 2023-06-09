using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CommandBuilder
{
    public static EmptyCommand EmptyCommand = new EmptyCommand();

    private Combatant actor;

    private CommandManager.ICommand command;

    private Battlezone startingZone;

    private Combatant targetCombatant;

    private Battlezone targetZone;

    public Combatant Actor { set => actor = value; }

    public CommandManager.ICommand Command { set => command = value; }

    public Battlezone StartingZone { set => startingZone = value; }

    public Combatant TargetCombatant { set => targetCombatant = value; }

    public Battlezone TargetZone { set => targetZone = value; }

    public void Clear()
    {
        Actor = null;
        Command = null;
        TargetZone = null;
        StartingZone = null;
        TargetCombatant = null;
    }

    public CommandManager.ICommand GetFinishedCommand()
    {
        Assert.IsNotNull(command);

        if (command is CommandManager.ITargetSelfCommand)
        {
            return CompletedSelfTargeter();
        }
        else if (command is CommandManager.ITargetZoneCommand)
        {
            return CompletedZoneTargeter();
        }
        else if (command is CommandManager.ITargetCombatantCommand)
        {
            return CompletedCombatantTargeter();
        }

        return EmptyCommand;
    }

    private CommandManager.ICommand CompletedCombatantTargeter()
    {
        CommandManager.ITargetCombatantCommand TCCommand = (CommandManager.ITargetCombatantCommand)command;

        if ((command != null) && (actor != null) && (targetCombatant != null))
        {
            TCCommand.Actor = actor;
            TCCommand.TargetCombatant = targetCombatant;
        }

        return TCCommand as CommandManager.ICommand;
    }

    private CommandManager.ICommand CompletedSelfTargeter()
    {
        CommandManager.ITargetSelfCommand TSCommand = (CommandManager.ITargetSelfCommand)command;

        if ((command != null) && (actor != null))
        {
            TSCommand.Actor = actor;
            return TSCommand as CommandManager.ICommand;
        }

        return EmptyCommand;
    }

    private CommandManager.ICommand CompletedZoneTargeter()
    {
        CommandManager.ITargetZoneCommand TZCommand = (CommandManager.ITargetZoneCommand)command;

        if ((command != null) && (actor != null) && (startingZone != null) && (targetZone != null))
        {
            TZCommand.Actor = actor;
            TZCommand.TargetZone = targetZone;
        }

        return TZCommand as CommandManager.ICommand;
    }
}