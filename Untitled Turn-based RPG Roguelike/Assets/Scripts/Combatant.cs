using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    private UnitProfile unit;
    public GameObject model;


    public virtual void EnterCombat(Battlezone zone, Transform t)
    {
        //Instantiate cobatant at the location
    }

    public virtual void MoveTo(Battlezone zone, Transform t)
    {
        //Move combatant to new zone and locaiton
    }
}
