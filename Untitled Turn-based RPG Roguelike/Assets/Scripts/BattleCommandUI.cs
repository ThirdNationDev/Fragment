using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleCommandUI : MonoBehaviour
{

    public TextMeshProUGUI combatantNameText;
    public TextMeshProUGUI combatantHealth;


    private void Awake()
    {
    }


    private void LateUpdate()
    {
        Combatant current = BattleManager.Instance.currentCombatant;
        combatantNameText.text = current.name;
        combatantHealth.text = current.stats.health.ToString() + "/" + current.stats.maxHealth.ToString();
    }

    public void MoveForwardOne()
    {
        
        BattleManager.Instance.currentCommand = BattleManager.Instance.currentCombatant.defendBCom;

        BattleManager.Instance.currentCombatant.MoveForwardOne();
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
