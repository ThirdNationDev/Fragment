using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantStats: MonoBehaviour
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

    void Awake()
    {
        health = maxHealth;
        AP = startingAP;

    }
}
