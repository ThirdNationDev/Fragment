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

    public static UIManager Instance { get; private set; }

    [SerializeField]
    private TargetSelectPanel targetSelectPanel;
    [SerializeField]
    private DisplayBattleCommandUI battleCommandPanel;

    public BattleCommand commandSelected;



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
    }

    private void DeactivateUserInput()
    {
        targetSelectPanel.gameObject.SetActive(false);
        battleCommandPanel.gameObject.SetActive(false);
    }

    private void Start()
    {

        //targetSelectPanel.Deactivate();
    }

    private void LateUpdate()
    {
        //if(this.commandSelected == null) 
        //{ 
        //    targetSelectPanel.Deactivate(); 
        //}
    }

    public void MoveForwardOne()
    {
        this.commandSelected = BattleManager.Instance.currentCombatant.moveCommand;
        this.commandSelected.SetTarget(BattleManager.Instance.currentCombatant.battlezone.nextzone);
        SendCommand();
    }

    public void MoveBackOne()
    {
        this.commandSelected = BattleManager.Instance.currentCombatant.moveCommand;
        this.commandSelected.SetTarget(BattleManager.Instance.currentCombatant.battlezone.prevzone);
        SendCommand();
    }

    public void Defend()
    {
        //BattleManager.Instance.commandSelected = 
        //    BattleManager.Instance.currentCombatant.defendCommand;
        this.commandSelected = BattleManager.Instance.currentCombatant.defendCommand;
        SendCommand();
    }

    public void LightSkill()
    {
        this.commandSelected = 
            BattleManager.Instance.currentCombatant.lightSkillCommand;
        targetSelectPanel.DisplayTargets();
    }

    public void MidSkill()
    {
        this.commandSelected =
            BattleManager.Instance.currentCombatant.midSkillCommand;
        targetSelectPanel.DisplayTargets();
    }

    public void HeavySkill()
    {
        this.commandSelected =
            BattleManager.Instance.currentCombatant.heavySkillCommand;
        targetSelectPanel.DisplayTargets();
    }

    public void SendCommand()
    {
        if(commandSelected == null) { return; }

        if (commandSelected.Ready())
        {
            //BattleManager.Instance.commandsToExecuteQueue.Enqueue(commandSelected);
            //commandSelected = null;
        }
    }

    public void Cancel()
    {
        targetSelectPanel.Deactivate();
    }

    internal void ActivateBattleUI()
    {
        battleCommandPanel.gameObject.SetActive(true);
    }
}
