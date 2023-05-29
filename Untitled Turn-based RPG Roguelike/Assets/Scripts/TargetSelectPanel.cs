using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class TargetSelectPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject targetButtonPrefab;
    ScrollView scrollview;
    List<Combatant> targets;

    void Awake()
    {
        scrollview = GameObject.Find("Target Scroll").GetComponent<ScrollView>();
    }

    public void Deactivate()
    {
        this.Clear();
        panel.SetActive(false);
    }

    public void Clear()
    {
        scrollview.Clear();
        targets.Clear();
    }

    public void DisplayTargets()
    {
        this.gameObject.SetActive(true);
        targets = BattleManager.Instance.currentCommand.getTargets();
        foreach(Combatant target in targets)
        {
            GameObject go = Instantiate(targetButtonPrefab);
            Button b = go.GetComponent<Button>();
            b.text = target.name;
            scrollview.Add(b);
        }
    }

    public Combatant SelectTarget(Combatant selected)
    {
        return selected;
        return null;
    }

 


}
