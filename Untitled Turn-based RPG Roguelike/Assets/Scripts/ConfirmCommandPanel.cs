using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmCommandPanel : MonoBehaviour
{
    public void Display()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    private void Start()
    {
        Hide();
    }
}