using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CombatantStats: ScriptableObject
{

    public int unitNumber;
    public int maxHealth;
    public int health;
    public int AP;
    public int startingAP;

    public int aggression;
    public int compassion;
    public int diligence;
    public int guile;

}
