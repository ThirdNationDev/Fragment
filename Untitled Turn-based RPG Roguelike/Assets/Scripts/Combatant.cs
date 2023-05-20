using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    private UnitProfile unit;
    public Battlezone battlezone;
    Combatant myCombatant;
    public CombatantStats stats;


    public virtual void EnterCombat(Battlezone zone)
    {
        //Instantiate commbatant at the location
        GameObject gameObject = Instantiate(this.gameObject, zone.transform.position, Quaternion.identity);
        myCombatant = gameObject.GetComponent<Combatant>();
        myCombatant.battlezone = zone;
        myCombatant.gameObject.transform.parent = zone.transform;

        stats = gameObject.GetComponent<CombatantStats>();
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
        Battlezone targetZone = BattleManager.Instance.battlefield.getZone(battlezone.zoneNumber + 1);
        MoveTo(targetZone);
    }

    public virtual void MoveBackOne()
    {
        Battlezone targetZone = BattleManager.Instance.battlefield.getZone(battlezone.zoneNumber - 1);
        MoveTo(targetZone);
    }
}
