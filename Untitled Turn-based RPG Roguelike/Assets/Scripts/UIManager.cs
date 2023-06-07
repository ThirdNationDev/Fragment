using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public CommandManager.ICommand commandSelected;

    [SerializeField]
    private DisplayBattleCommandUI battleCommandPanel;

    [SerializeField]
    private TargetSelectPanel targetSelectPanel;

    public static UIManager Instance { get; private set; }

    public void Cancel()
    {
        targetSelectPanel.Deactivate();
    }

    public void Defend()
    {
        CommandManager.Instance.AddCommand(BattleManager.Instance.currentCombatant.defendCommand);
    }

    public void HeavySkill()
    {
        //this.commandSelected =
        //    BattleManager.Instance.currentCombatant.heavySkillCommand;
        //targetSelectPanel.DisplayTargets();
    }

    public void LightSkill()
    {
        //this.commandSelected =
        //    BattleManager.Instance.currentCombatant.lightSkillCommand;
        //targetSelectPanel.DisplayTargets();
        targetSelectPanel.DisplayTargetsForCommand(BattleManager.Instance.currentCombatant.lightSkillCommand);
    }

    public void MidSkill()
    {
        //this.commandSelected =
        //    BattleManager.Instance.currentCombatant.midSkillCommand;
        //targetSelectPanel.DisplayTargets();
    }

    public void MoveBackOne()
    {
        MoveCombatant(BattleManager.Instance.currentCombatant.battlezone.prevzone);
    }

    public void MoveForwardOne()
    {
        MoveCombatant(BattleManager.Instance.currentCombatant.battlezone.nextzone);
    }

    public void SendCommand()
    {
        //if (commandSelected == null) { return; }

        //if (commandSelected.Ready())
        //{
        //    //BattleManager.Instance.commandsToExecuteQueue.Enqueue(commandSelected);
        //    //commandSelected = null;
        //}
    }

    internal void ActivateBattleUI()
    {
        battleCommandPanel.gameObject.SetActive(true);
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

        Assert.IsNotNull(targetSelectPanel);
        Assert.IsNotNull(battleCommandPanel);

        DeactivateUserInput();
        commandSelected = CommandManager.EmptyCommand;
    }

    private void DeactivateUserInput()
    {
        targetSelectPanel.gameObject.SetActive(false);
        battleCommandPanel.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        //if(this.commandSelected == null)
        //{
        //    targetSelectPanel.Deactivate();
        //}
    }

    private void MoveCombatant(Battlezone target)
    {
        Combatant actor = BattleManager.Instance.currentCombatant;
        if (actor.CanMoveTo(target))
        {
            CommandManager.ITargetZoneCommand command = actor.moveCommand as CommandManager.ITargetZoneCommand;
            command.SetActor(actor);
            command.SetTarget(target);
            CommandManager.Instance.AddCommand(command);
        }
    }

    private void Start()
    {
        //targetSelectPanel.Deactivate();
    }
}