using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    private float aggressionDamage;
    private float compassionDamage;
    private float disciplineDamage;
    private float guileDamage;

    public Damage(float agg, float com, float dis, float gui)
    {
        aggressionDamage = agg;
        compassionDamage = com;
        disciplineDamage = dis;
        guileDamage = gui;
    }

    internal float ResultingDamageFromTo(Combatant combatant, Combatant target)
    {
        return 0;
    }

    //public float DamageReceivedBy(Combatant target)
    //{
    //    return 10;
    //}

    //public Damage DamageDealtBy(Combatant dealer) { }

    //public static Damage
}