using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayBattleCommandUI : MonoBehaviour
{

    public TextMeshProUGUI combatantNameText;
    public TextMeshProUGUI combatantHealth;
    Combatant currentCombatant;
    public TargetSelectPanel targetSelectPanel;

    public Button lightAttackButton;
    public Button midAttackButton;
    public Button heavyAttackButton;


    private void Start()
    {
        currentCombatant = BattleManager.Instance.currentCombatant;


        TextMeshProUGUI lightAttackButtonText = lightAttackButton.GetComponentInChildren<TextMeshProUGUI>();
        lightAttackButtonText.text = currentCombatant.lightSkill.name;
    }


    private void LateUpdate()
    {
        //currentcombatant = battlemanager.instance.currentcombatant;
        //combatantnametext.text = currentcombatant.name;
        //combatantHealth.text = currentCombatant.stats.health.ToString() + "/" + currentCombatant.stats.maxHealth.ToString();
    }



}
