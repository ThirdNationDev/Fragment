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

public class BasicMoveSkill : MonoBehaviour, IEquipableSkill
{
    [SerializeField]
    private GameObject icon;

    [SerializeField]
    private Animation skillAnimation;

    [SerializeField]
    private SkillStats skillStats;

    public GameObject Icon { get => icon; }
    public Animation SkillAnimation { get => skillAnimation; }
    public SkillStats SkillStats { get => skillStats; }

    public CommandManager.ICommand Command()
    {
        CommandManager.ICommand command = new MoveCommand();
        command.Skill = this;
        return command;
    }
}