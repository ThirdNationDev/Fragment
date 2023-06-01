using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManagerPanel : MonoBehaviour
{

    public TextMeshProUGUI currentStateTurn;
    public TextMeshProUGUI commandLogCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentStateTurn.text = "State: " + BattleManager.Instance.battleStateController.ToString()
            + ", Turn: " + BattleManager.Instance.turnCtr.ToString();

        commandLogCount.text = "Command Log Count: " + BattleManager.Instance.executedCommandStack.Count;
    }
}
