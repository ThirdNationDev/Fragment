using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CommandLog : MonoBehaviour
{
    [SerializeField]
    private CommandLogEntry commandLogEntryPrefab;

    private int lastCount;

    [SerializeField]
    private ScrollRect scroller;

    [SerializeField]
    private GameObject scrollerContent;

    private void AddNewLogEntry(CommandManager.ICommand command)
    {
        CommandLogEntry entry = Instantiate<CommandLogEntry>(commandLogEntryPrefab);
        entry.transform.SetParent(scrollerContent.transform);

        entry.combatantText.text = BattleManager.Instance.currentCombatant.ToString();
        entry.commandText.text = command.ToString();
        entry.commandNumberText.text = lastCount.ToString().PadLeft(3, '0');
    }

    // Start is called before the first frame update
    private void Awake()
    {
        lastCount = 0;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        while (CommandManager.Instance.CommandCount > lastCount)
        {
            int commandIndex = CommandManager.Instance.CommandCount - 1 - lastCount;  //Stack array indexing puts 0 at most recent
            AddNewLogEntry(CommandManager.Instance.CommandAtIndex(commandIndex));
            lastCount++;
        }

        scroller.velocity = new Vector2(0f, 1000f);
    }
}