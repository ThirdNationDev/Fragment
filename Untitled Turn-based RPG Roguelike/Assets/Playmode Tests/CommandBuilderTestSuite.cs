using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CommandBuilderTestSuite
{
    public static CommandBuilder CommandBuilder = new CommandBuilder();
    private GameObject playerCombatantPrefab = Resources.Load<GameObject>("Prefabs/Pinky");

    [Test]
    public void TestClear()
    {
        Debug.Log("Test clear called.");
        Combatant combatant = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        CommandBuilder.SetCommand(combatant.defendCommand);
        CommandBuilder.SetActor(combatant);
        CommandBuilder.SetTarget(combatant);

        CommandBuilder.Clear();

        Assert.IsNull(CommandBuilder.Command);
    }

    [Test]
    public void TestGetFinishedCommand()
    {
        Combatant combatant = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        CommandBuilder.SetCommand(combatant.defendCommand);
        CommandBuilder.SetActor(combatant);
        CommandBuilder.SetTarget(combatant);

        CommandManager.ICommand command = CommandBuilder.GetFinishedCommand();

        Assert.AreEqual(combatant, command.Target);
        Assert.AreEqual(command, command);
        Assert.AreEqual(combatant, command.Actor);
    }

    [Test]
    public void TestSetActor()
    {
        Combatant combatant = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        CommandBuilder.SetCommand(combatant.defendCommand);
        CommandBuilder.SetActor(combatant);

        Assert.AreEqual(combatant, CommandBuilder.Command.Actor);
    }

    [Test]
    public void TestSetCommand()
    {
        Combatant combatant = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        CommandManager.ICommand command = combatant.defendCommand;
        CommandBuilder.SetCommand(command);

        Assert.AreEqual(command, CommandBuilder.Command);
    }

    [Test]
    public void TestSetTarget()
    {
        Combatant combatant = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        CommandBuilder.SetCommand(combatant.defendCommand);
        CommandBuilder.SetTarget(combatant);

        Assert.AreEqual(combatant, CommandBuilder.Command.Target);
    }
}