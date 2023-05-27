using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleCommandUI : MonoBehaviour
{

    public TextMeshProUGUI combatantNameText;
    public TextMeshProUGUI combatantHealth;
    Combatant currentCombatant;


    private void Awake()
    {
    }


    private void LateUpdate()
    {
        currentCombatant = BattleManager.Instance.currentCombatant;
        combatantNameText.text = currentCombatant.name;
        combatantHealth.text = currentCombatant.stats.health.ToString() + "/" + currentCombatant.stats.maxHealth.ToString();
    }

    public void MoveForwardOne()
    {
        
        BattleCommand moveForward = currentCombatant.movementCommand;
        moveForward.SetTarget(currentCombatant.battlezone.nextzone);
        BattleManager.Instance.currentCommand = moveForward;
    }

    public void MoveBackOne()
    {
        BattleManager.Instance.currentCombatant.MoveBackOne();
    }

    public void Defend()

    {

        BattleManager.Instance.currentCommand = BattleManager.Instance.currentCombatant.defendBCom;
    }


}
