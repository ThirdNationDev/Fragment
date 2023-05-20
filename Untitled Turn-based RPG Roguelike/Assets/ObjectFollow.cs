using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{

    public Transform Follow;

    public Vector3 initialOffset;

    private Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var screenPos = MainCamera.WorldToScreenPoint(Follow.position);

        transform.position = screenPos + initialOffset;
    }
}
