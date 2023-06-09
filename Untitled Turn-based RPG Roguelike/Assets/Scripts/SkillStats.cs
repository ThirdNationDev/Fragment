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

[CreateAssetMenu]
public class SkillStats : ScriptableObject
{
    [SerializeField]
    private float flatDamage;

    [SerializeField]
    private int range;

    public int Range { get => range; }

    public Damage SkillDamage()
    {
        Damage skillDamage = new();
        skillDamage.FlatDamage = flatDamage;
        return skillDamage;
    }
}