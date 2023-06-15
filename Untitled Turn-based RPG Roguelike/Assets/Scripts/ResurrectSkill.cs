using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectSkill : MonoBehaviour, IEquipableSkill
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
        CommandManager.ICommand command = new ResurrectCommand();
        command.Skill = this;
        return command;
    }
}