using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    private UnitProfile unit;
    public Battlezone battlezone;
    Combatant myCombatant;

    public virtual void EnterCombat(Battlezone zone)
    {
        //Instantiate cobatant at the location
        GameObject gameObject = Instantiate(this.gameObject, zone.transform.position, Quaternion.identity);
        myCombatant = gameObject.GetComponent<Combatant>();
        myCombatant.battlezone = zone;
        myCombatant.gameObject.transform.parent = zone.transform;
    }

    public virtual void MoveTo(Battlezone newZone)
    {
        //Move combatant to new zone and locaiton
        this.transform.position = newZone.transform.position;
        battlezone = newZone;
    }
}
