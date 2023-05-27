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
    public int maxRange;
    public int stepsRemaining;

    public int aggression;
    public int compassion;
    public int discipline;
    public int guile;

}
