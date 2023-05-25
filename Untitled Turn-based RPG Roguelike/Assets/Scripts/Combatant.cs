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
        zone.AddCombatant(this);

        stats.AP = stats.startingAP;
        stats.health = stats.maxHealth;
        stats.range = stats.maxRange;

    }

    public virtual void MoveTo(Battlezone newZone)
    {
        int distance = Mathf.Abs(battlezone.zoneNumber - newZone.zoneNumber);
        //Move combatant to new zone and locaiton
        if (distance <= stats.range)
        {
            battlezone.RemoveCombatant(this.gameObject);
            newZone.AddCombatant(this);
            battlezone = newZone;
            stats.AP--;
        }

    }

    public virtual void MoveForwardOne()
    {
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
