using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattlezoneSimple : MonoBehaviour
{
    [SerializeField]
    private Vector3 scale;

    public int zoneNumber;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
