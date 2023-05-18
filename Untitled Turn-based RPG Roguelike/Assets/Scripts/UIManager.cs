using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public BattleCommand currentCommand { get; private set; }

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        currentCommand = null;
    }

    void OnConfirm()
    {
        currentCommand = new ConfirmBCom();
    }

}
