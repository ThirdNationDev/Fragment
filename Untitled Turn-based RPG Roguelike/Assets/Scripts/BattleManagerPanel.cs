using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManagerPanel : MonoBehaviour
{

    public TextMeshProUGUI currentStateTurn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentStateTurn.text = "State: " + BattleManager.Instance.battleStateController.ToString()
            + ", Turn: " + BattleManager.Instance.turnCtr.ToString();
    }
}
