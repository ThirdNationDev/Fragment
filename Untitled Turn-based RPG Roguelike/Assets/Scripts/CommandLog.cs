using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CommandLog : MonoBehaviour
{
    public TextMeshProUGUI commandLog;
    public ScrollRect scroller;
    string logtext;
    BattleCommand currentCommand;

    int lastCount;


    // Start is called before the first frame update
    void Awake()
    {
        logtext = "";
        currentCommand = null;

        lastCount = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //if(BattleManager.Instance.executedCommandStack.Count > lastCount)
        //{
        //    currentCommand = BattleManager.Instance.executedCommandStack.Peek();
        //    logtext += currentCommand.ToString();
        //    lastCount = BattleManager.Instance.executedCommandStack.Count;
        //    scroller.velocity = new Vector2(0f, 1000f);
        //}

        //commandLog.text = logtext;
    }
}
