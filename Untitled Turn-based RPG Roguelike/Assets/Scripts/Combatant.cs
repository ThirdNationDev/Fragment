using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    private UnitProfile unit;
    public GameObject model;
    public Battlezone battlezone;

    public virtual void EnterCombat(Battlezone zone)
    {
        //Instantiate cobatant at the location
    }

    public virtual void MoveTo(Battlezone newZone)
    {
        //Move combatant to new zone and locaiton
        model.transform.position = newZone.transform.position;
        battlezone = newZone;
    }
}
