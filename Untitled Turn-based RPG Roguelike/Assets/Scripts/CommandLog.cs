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
    BattleCommand lastCommand;
    BattleCommand currentCommand;

    int lastCount;


    // Start is called before the first frame update
    void Awake()
    {
        logtext = "";
        lastCommand = null;
        currentCommand = null;

        lastCount = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if (BattleManager.Instance.commandList.Count > 0)
        //{
        //    currentCommand = BattleManager.Instance.commandList.Peek();
        //    if (currentCommand != lastCommand)
        //    {
        //        logtext += currentCommand.ToString();
        //        lastCommand = currentCommand;
        //        scroller.velocity = new Vector2(0f, 1000f);
        //    }
        //}

        if(BattleManager.Instance.commandList.Count > lastCount)
        {
            currentCommand = BattleManager.Instance.commandList.Peek();
            logtext += currentCommand.ToString();
            lastCount = BattleManager.Instance.commandList.Count;
            scroller.velocity = new Vector2(0f, 1000f);
        }

        commandLog.text = logtext;
    }
}
