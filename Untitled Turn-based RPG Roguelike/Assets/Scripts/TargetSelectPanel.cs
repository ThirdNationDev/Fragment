using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetSelectPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject targetButtonPrefab;
    public RectTransform scrollContent;
    List<Combatant> targets;
    List<GameObject> buttons;

    void Awake()
    {
        targets = new List<Combatant>();
        buttons = new List<GameObject>();
    }

    public void Deactivate()
    {
        Clear();
        BattleManager.Instance.commandSelected = null;
        panel.SetActive(false);
    }

    public void Clear()
    {
        targets.Clear();
        foreach(GameObject go in buttons)
        {
            Destroy(go);
        }
        buttons.Clear();

    }

    public void DisplayTargets()
    {
        Clear();
        this.gameObject.SetActive(true);
        targets = BattleManager.Instance.commandSelected.getTargets();
        Debug.Log(targets.Count);
        foreach(Combatant target in targets)
        {
            GameObject go = Instantiate(targetButtonPrefab);
            go.transform.SetParent(scrollContent.transform);
            Button b = go.GetComponent<Button>();
            TextMeshProUGUI buttonText = b.GetComponentInChildren<TextMeshProUGUI>();
            b.onClick.AddListener(delegate { SelectTarget(target); });
            buttonText.text = target.name;
            buttons.Add(go);
            
        }
    }

    public void SelectTarget(Combatant selected)
    {
        BattleManager.Instance.commandSelected.SetTarget(selected);
        UIManager.Instance.SendCommand();
    }

 


}
