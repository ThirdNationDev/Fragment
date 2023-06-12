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

public class CommandBuilder
{
    public static EmptyCommand EmptyCommand = new EmptyCommand();

    private CommandManager.ICommand command;

    public CommandManager.ICommand Command { get => command; }

    public static DeathCommand DeathCommand(Combatant victim)
    {
        Assert.IsNotNull(victim);

        DeathCommand command = new();
        command.Actor = victim;
        command.Target = victim;
        return command;
    }

    public void Clear()
    {
        command = null;
    }

    public CommandManager.ICommand GetFinishedCommand()
    {
        Assert.IsNotNull(Command);
        Assert.IsNotNull(Command.Actor);
        Assert.IsNotNull(Command.Target);

        return Command;
    }

    public void SetActor(Combatant combatant)
    {
        Assert.IsNotNull(Command);
        Assert.IsNotNull(combatant);

        Command.Actor = combatant;
    }

    public void SetCommand(CommandManager.ICommand command)
    {
        Assert.IsNotNull(command);

        this.command = command;
    }

    public void SetTarget(ITargetable target)
    {
        Assert.IsNotNull(Command);
        Assert.IsNotNull(target);

        Command.Target = target;
    }
}