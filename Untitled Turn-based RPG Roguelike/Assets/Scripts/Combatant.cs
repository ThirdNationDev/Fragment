using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    private UnitProfile unit;
    public Battlezone battlezone;
    public CombatantStats stats;


    public virtual void EnterCombat(Battlezone zone)
    {
        battlezone = zone;
        transform.position = zone.transform.position;
        transform.parent = zone.transform;

        stats.AP = stats.startingAP;
        stats.health = stats.maxHealth;

    }

    public virtual void MoveTo(Battlezone newZone)
    {
        //Move combatant to new zone and locaiton
        if (stats.AP > 0)
        {
            this.transform.position = newZone.transform.position;
            this.transform.parent = newZone.transform;
            battlezone = newZone;
            stats.AP--;
        }

    }

    public virtual void MoveForwardOne()
    {
        Debug.Log("BMI: " + BattleManager.Instance);
        Debug.Log("BMIB: " + BattleManager.Instance.battlefield);
        Debug.Log("BMIBZ: " + BattleManager.Instance.battlefield.getZone(0));
        Debug.Log("BZ: " + battlezone);
        Battlezone targetZone = BattleManager.Instance.battlefield.getZone(battlezone.zoneNumber + 1);
        ChangeZoneBCom command = new ChangeZoneBCom(this, targetZone);
        BattleManager.Instance.ExecuteCommand(command);
        //MoveTo(targetZone);
    }

    public virtual void MoveBackOne()
    {
        Battlezone targetZone = BattleManager.Instance.battlefield.getZone(battlezone.zoneNumber - 1);
        ChangeZoneBCom command = new ChangeZoneBCom(this, targetZone);
        BattleManager.Instance.ExecuteCommand(command);
    }
}
