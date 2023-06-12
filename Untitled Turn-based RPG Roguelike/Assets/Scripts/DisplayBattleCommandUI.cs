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
using UnityEngine.UI;
using TMPro;

public class DisplayBattleCommandUI : MonoBehaviour
{
    public TextMeshProUGUI combatantHealth;
    public TextMeshProUGUI combatantNameText;
    public Button heavyAttackButton;
    public Button lightAttackButton;
    public Button midAttackButton;
    public TargetSelectPanel targetSelectPanel;
    private Combatant currentCombatant;

    private void LateUpdate()
    {
        currentCombatant = BattleManager.Instance.currentCombatant;
        combatantNameText.text = currentCombatant.name;
        combatantHealth.text = currentCombatant.Stats.health.ToString() + "/" + currentCombatant.Stats.maxHealth.ToString();
    }

    private void Start()
    {
        currentCombatant = BattleManager.Instance.currentCombatant;

        TextMeshProUGUI lightAttackButtonText = lightAttackButton.GetComponentInChildren<TextMeshProUGUI>();
        lightAttackButtonText.text = currentCombatant.lightSkill.name;
    }
}