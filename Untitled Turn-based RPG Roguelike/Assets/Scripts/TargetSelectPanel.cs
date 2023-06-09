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
using UnityEngine.UI;
using TMPro;
using System;

public class TargetSelectPanel : MonoBehaviour
{
    public GameObject panel;
    public RectTransform scrollContent;
    public GameObject targetButtonPrefab;
    private List<GameObject> buttons;
    private List<ITargetable> targets;

    public void ClearTargets()
    {
        targets.Clear();
        foreach (GameObject go in buttons)
        {
            Destroy(go);
        }
        buttons.Clear();
    }

    public void Deactivate()
    {
        ClearTargets();
        UIManager.CommandBuilder.Clear();
        panel.SetActive(false);
    }

    internal void DisplayTargetsForCommand(CommandManager.ICommand command)
    {
        Assert.IsNotNull(command);

        ClearTargets();
        this.gameObject.SetActive(true);
        targets = command.PotentialTargets();
        foreach (ITargetable target in targets)
        {
            Button b = createButtonInScroll();
            assignTargetToButton(b, target);
        }
    }

    private void assignTargetToButton(Button b, ITargetable target)
    {
        Assert.IsNotNull(b);
        Assert.IsNotNull(target);

        TextMeshProUGUI buttonText = b.GetComponentInChildren<TextMeshProUGUI>();
        b.onClick.AddListener(delegate { UIManager.Instance.OnTargetSelect(target); });
        buttonText.text = target.Name;
    }

    private void Awake()
    {
        targets = new List<ITargetable>();
        buttons = new List<GameObject>();
    }

    private Button createButtonInScroll()
    {
        GameObject go = Instantiate(targetButtonPrefab);
        go.transform.SetParent(scrollContent.transform);
        buttons.Add(go);
        return go.GetComponent<Button>();
    }
}