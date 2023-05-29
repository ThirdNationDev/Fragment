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
        Move(currentCombatant.battlezone.nextzone);

    }

    public void MoveBackOne()
    {
        Move(currentCombatant.battlezone.prevzone);
    }

    private void Move(Battlezone target)
    {
        BattleCommand move = currentCombatant.moveCommand;
        move.SetTarget(target);
        BattleManager.Instance.currentCommand = move;
    }


    public void Defend()

    { 
        BattleManager.Instance.currentCommand = BattleManager.Instance.currentCombatant.defendCommand;
    }


}
