using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class CommandManagerTestSuite
{
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Scenes/BattleDev v0-1");
        yield return null;
    }

    [Test]
    public void TestAddAndExcecuteCommand()
    {
        CommandManager.ICommand command = getDefendCommand();
        CommandManager.Instance.AddAndExecuteCommand(command);

        Assert.AreEqual(command, CommandManager.Instance.LastCommand);
    }

    [Test]
    public void TestWhenExcecuteCommandShouldEndTheTurn()
    {
        CommandManager.ICommand command = getDefendCommand(); //ends the turn
        int turn1 = BattleManager.Instance.turnCtr;
        CommandManager.Instance.TestExecuteCommand(command);
        int turn2 = BattleManager.Instance.turnCtr;

        Assert.AreEqual(turn1, turn2 - 1);
    }

    [Test]
    public void TestWhenExcecuteCommandShouldNOTEndTheTurn()
    {
        CommandManager.ICommand command = getMoveCommand();   //does not end the turn
        int turn1 = BattleManager.Instance.turnCtr;
        CommandManager.Instance.TestExecuteCommand(command);
        int turn2 = BattleManager.Instance.turnCtr;
        Debug.Log("Test :" + turn1 + " and " + turn2);
        Assert.AreEqual(turn1, turn2);
    }

    private CommandManager.ICommand getDefendCommand()
    {
        UIManager.CommandBuilder.SetCommand(BattleManager.Instance.currentCombatant.defendCommand);
        UIManager.CommandBuilder.SetActor(BattleManager.Instance.currentCombatant);
        UIManager.CommandBuilder.SetTarget(BattleManager.Instance.currentCombatant);

        CommandManager.ICommand command = UIManager.CommandBuilder.GetFinishedCommand();
        UIManager.CommandBuilder.Clear();
        return command;
    }

    private CommandManager.ICommand getMoveCommand()
    {
        UIManager.CommandBuilder.SetCommand(BattleManager.Instance.currentCombatant.moveCommand);
        UIManager.CommandBuilder.SetActor(BattleManager.Instance.currentCombatant);
        UIManager.CommandBuilder.SetTarget(BattleManager.Instance.currentCombatant.battlezone);

        CommandManager.ICommand command = UIManager.CommandBuilder.GetFinishedCommand();
        UIManager.CommandBuilder.Clear();
        return command;
    }
}