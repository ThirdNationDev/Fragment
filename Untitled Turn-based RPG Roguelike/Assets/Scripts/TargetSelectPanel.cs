using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class TargetSelectPanel : MonoBehaviour
{
    public GameObject panel;
    public Button targetButtonPrefab;
    public ScrollView scrollview;
    List<Combatant> targets;

   public void Deactivate()
    {
        panel.SetActive(false);
        this.Clear();
    }

    public void Clear()
    {
        scrollview.Clear();
        targets.Clear();
    }

    public void DisplayTargets()
    {

    }

    public Combatant SelectTarget(Combatant selected)
    {
        return selected;
        return null;
    }

 


}
