using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    public TargetSelectPanel targetSelectPanel;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

    }

    private void Start()
    {

        targetSelectPanel.Deactivate();
    }

    private void LateUpdate()
    {
        if(BattleManager.Instance.commandSelected == null) 
        { 
            targetSelectPanel.Deactivate(); 
        }
    }

    public void MoveForwardOne()
    {
        BattleCommand command = BattleManager.Instance.currentCombatant.moveCommand;
        command.SetTarget(BattleManager.Instance.currentCombatant.battlezone.nextzone);
        BattleManager.Instance.commandSelected = command;
        SendCommand();
    }

    public void MoveBackOne()
    {
        BattleCommand command = BattleManager.Instance.currentCombatant.moveCommand;
        command.SetTarget(BattleManager.Instance.currentCombatant.battlezone.prevzone);
        BattleManager.Instance.commandSelected = command;
        SendCommand();
    }

    public void Defend()
    {
        BattleManager.Instance.commandSelected = 
            BattleManager.Instance.currentCombatant.defendCommand;
        SendCommand();
    }

    public void LightSkill()
    {
        BattleManager.Instance.commandSelected = 
            BattleManager.Instance.currentCombatant.lightSkillCommand;
        targetSelectPanel.DisplayTargets();
    }

    public void MidSkill()
    {
        BattleManager.Instance.commandSelected =
            BattleManager.Instance.currentCombatant.midSkillCommand;
        targetSelectPanel.DisplayTargets();
    }

    public void HeavySkill()
    {
        BattleManager.Instance.commandSelected =
            BattleManager.Instance.currentCombatant.heavySkillCommand;
        targetSelectPanel.DisplayTargets();
    }

    public void SendCommand()
    {
        BattleManager.Instance.commandToExecute =
            BattleManager.Instance.commandSelected;
        BattleManager.Instance.commandSelected = null;
    }

    public void Cancel()
    {
        targetSelectPanel.Deactivate();
    }

}
