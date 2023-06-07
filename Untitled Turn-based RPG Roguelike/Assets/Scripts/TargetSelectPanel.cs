using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TargetSelectPanel : MonoBehaviour
{
    public GameObject panel;
    public RectTransform scrollContent;
    public GameObject targetButtonPrefab;
    private List<GameObject> buttons;
    private List<Combatant> targets;

    public void Clear()
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
        Clear();
        UIManager.Instance.commandSelected = null;
        panel.SetActive(false);
    }

    public void DisplayTargets()
    {
        //Clear();
        //this.gameObject.SetActive(true);
        //targets = UIManager.Instance.commandSelected.getTargets();
        //foreach (Combatant target in targets)
        //{
        //    GameObject go = Instantiate(targetButtonPrefab);
        //    go.transform.SetParent(scrollContent.transform);
        //    Button b = go.GetComponent<Button>();
        //    TextMeshProUGUI buttonText = b.GetComponentInChildren<TextMeshProUGUI>();
        //    b.onClick.AddListener(delegate { SelectTarget(target); });
        //    buttonText.text = target.name;
        //    buttons.Add(go);
        //}
    }

    public void SelectTarget(Combatant selected)
    {
        //UIManager.Instance.commandSelected.SetTarget(selected);
        //UIManager.Instance.SendCommand();
    }

    internal void DisplayTargetsForCommand(CommandManager.ICommand lightSkillCommand)
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        targets = new List<Combatant>();
        buttons = new List<GameObject>();
    }
}