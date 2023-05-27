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


    // Start is called before the first frame update
    void Awake()
    {
        logtext = "";
        lastCommand = null;
        currentCommand = null;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (BattleManager.Instance.commandList.Count > 0)
        {
            currentCommand = BattleManager.Instance.commandList.Peek();
        }
        if(currentCommand != lastCommand)
        {
            logtext += currentCommand.ToString();
            lastCommand = currentCommand;
            scroller.velocity = new Vector2(0f, 1000f);
        }
        commandLog.text = logtext;
    }
}
