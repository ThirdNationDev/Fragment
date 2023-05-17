using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Battlezone : MonoBehaviour
{
    [SerializeField]
    private Vector3 scale;

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
