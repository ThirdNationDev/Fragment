/* Copyright (C) 2023 Thomas Payne, Third Nation Games - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the Third Nation Games license, which unfortunately won't be
 * written for another century.
 *
 * You should have received a copy of the Third Nation Games license with
 * this file. If not, please write to: dev@thirdnationgames.com, or visit : www.thirdnationgames.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public static class BattleCalculator
{
    public static float CalculateDamage(Combatant attacker, Combatant target, Damage skillDamage)
    {
        return 1;
    }

    public static bool DoesStatusEffectAttach(Combatant attacker, Combatant target, StatusAttack statusAttack)
    {
        return true;
    }

    public static Stack<int> RandomNonrepeatingInts(int startInclusive, int endInclusive, int count)
    {
        Stack<int> numbers = new Stack<int>();

        while (numbers.Count < count)
        {
            int num = Random.Range(startInclusive, endInclusive + 1); //Random.Range is end exclusive
            if (!numbers.Contains(num))
            {
                numbers.Push(num);
            }
        }

        return numbers;
    }
}