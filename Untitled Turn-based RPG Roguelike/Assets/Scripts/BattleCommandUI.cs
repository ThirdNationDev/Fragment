using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleCommandUI : MonoBehaviour
{

    public TextMeshProUGUI combatantNameText;
    public TextMeshProUGUI combatantHealth;
    Combatant currentCombatant;
    public GameObject targettingPanel;

    public Button lightAttackButton;
    public Button midAttackButton;
    public Button heavyAttackButton;

    BattleCommand selectedCommand;
    Battlezone zoneTarget;
    Combatant combatTarget;

    private void Start()
    {
        currentCombatant = BattleManager.Instance.currentCombatant;
        selectedCommand = null;
        zoneTarget = null;
        combatTarget = null;

        TextMeshProUGUI lightAttackButtonText = lightAttackButton.GetComponentInChildren<TextMeshProUGUI>();
        lightAttackButtonText.text = currentCombatant.lightSkill.name;
        targettingPanel.SetActive(false);
    }


    private void LateUpdate()
    {
        currentCombatant = BattleManager.Instance.currentCombatant;
        combatantNameText.text = currentCombatant.name;
        combatantHealth.text = currentCombatant.stats.health.ToString() + "/" + currentCombatant.stats.maxHealth.ToString();
    }

    public void MoveForwardOne()
    {
        selectedCommand = currentCombatant.moveCommand;
        zoneTarget = currentCombatant.battlezone.nextzone;
        SendCommand();
    }

    public void MoveBackOne()
    {
        selectedCommand = currentCombatant.moveCommand;
        zoneTarget = currentCombatant.battlezone.prevzone;
        SendCommand();
    }

    public void Defend()
    { 
        selectedCommand = currentCombatant.defendCommand;
        SendCommand();
    }

    public void LightSkill()
    {
        targettingPanel.SetActive(true);
        selectedCommand = currentCombatant.lightSkillCommand;
    }

    public void MidSkill()
    {

    }

    public void HeavySkill()
    {

    }

    void SendCommand()
    {
        if(zoneTarget != null) { selectedCommand.SetTarget(zoneTarget); }

        if(combatTarget != null) { selectedCommand.SetTarget(combatTarget); }

        BattleManager.Instance.currentCommand = selectedCommand;

        this.selectedCommand = null;
        this.zoneTarget = null;
        this.combatTarget = null;
    }
}
