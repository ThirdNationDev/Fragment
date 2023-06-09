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
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static CommandBuilder CommandBuilder = new CommandBuilder();

    [SerializeField]
    private DisplayBattleCommandUI battleCommandPanel;

    [SerializeField]
    private ConfirmCommandPanel confirmCommandPanel;

    [SerializeField]
    private TargetSelectPanel targetSelectPanel;

    public static UIManager Instance { get; private set; }

    private Combatant currentCombatant
    {
        get { return BattleManager.Instance.currentCombatant; }
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
        targetSelectPanel.DisplayTargetsForCommand(currentCombatant.lightSkillCommand);
    }

    public void MidSkill()
    {
        //this.commandSelected =
        //    BattleManager.Instance.currentCombatant.midSkillCommand;
        //targetSelectPanel.DisplayTargets();
    }

    public void OnCancel()
    {
        targetSelectPanel.Deactivate();
        CommandBuilder.Clear();
        confirmCommandPanel.Hide();
    }

    public void OnConfirmCommand()
    {
        ConfirmCommand();
    }

    public void OnDefend()
    {
        CommandBuilder.Command = currentCombatant.defendCommand;
        CommandBuilder.Actor = currentCombatant;
        confirmCommandPanel.Display();
    }

    public void OnMoveBackOne()
    {
        MoveCombatant(currentCombatant.battlezone.prevzone);
    }

    public void OnMoveForwardOne()
    {
        MoveCombatant(currentCombatant.battlezone.nextzone);
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
        CommandBuilder.Clear();
    }

    private void ConfirmCommand()
    {
        CommandManager.Instance.AddCommand(CommandBuilder.GetFinishedCommand());
        CommandBuilder.Clear();
        confirmCommandPanel.Hide();
    }

    private void DeactivateUserInput()
    {
        targetSelectPanel.gameObject.SetActive(false);
        battleCommandPanel.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
    }

    private void MoveCombatant(Battlezone target)
    {
        if (currentCombatant.CanMoveTo(target))
        {
            CommandBuilder.Command = currentCombatant.moveCommand;
            CommandBuilder.Actor = currentCombatant;
            CommandBuilder.StartingZone = currentCombatant.battlezone;
            CommandBuilder.TargetZone = target;

            ConfirmCommand();
        }
    }

    private void Start()
    {
    }
}