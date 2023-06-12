using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Assertions;

public class CommandLog : MonoBehaviour
{
    [SerializeField]
    private CommandLogEntry commandLogEntryPrefab;

    private int lastCount;

    [SerializeField]
    private ScrollRect scroller;

    [SerializeField]
    private GameObject scrollerContent;

    public void TestAddLogEntry(CommandManager.ICommand command)
    {
        AddNewLogEntry(command);
    }

    public CommandLogEntry TestCreateLogEntry(CommandManager.ICommand command)
    {
        return CreateLogEntry(command);
    }

    public void TestInitialize()
    {
        Initialize();
    }

    private void AddNewLogEntry(CommandManager.ICommand command)
    {
        Assert.IsNotNull(command);
        CommandLogEntry entry = CreateLogEntry(command);
        entry.transform.SetParent(scrollerContent.transform);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        Initialize();
    }

    private CommandLogEntry CreateLogEntry(CommandManager.ICommand command)
    {
        Assert.IsNotNull(command);
        CommandLogEntry entry = Instantiate<CommandLogEntry>(commandLogEntryPrefab);
        entry.combatantText.text = command.Actor.ToString();
        entry.commandText.text = command.ToString();
        entry.commandNumberText.text = lastCount.ToString().PadLeft(3, '0');
        return entry;
    }

    private void Initialize()
    {
        Assert.IsNotNull(commandLogEntryPrefab);
        Assert.IsNotNull(scroller);
        Assert.IsNotNull(scrollerContent);
        lastCount = 0;
    }

    private void LateUpdate()
    {
        while (CommandManager.Instance.CommandCount > lastCount)
        {
            int commandIndex = CommandManager.Instance.CommandCount - 1 - lastCount;  //Stack array indexing puts 0 at most recent
            AddNewLogEntry(CommandManager.Instance.CommandAtIndex(commandIndex));
            lastCount++;
        }

        scroller.velocity = new Vector2(0f, 1000f);  //Moves the scroller rapidly to the bottom
    }
}