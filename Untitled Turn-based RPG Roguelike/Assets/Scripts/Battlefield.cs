using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    [SerializeField]
    private Battlezone battleZonePrefab;

    [SerializeField]
    private int numberOfZones;

    private Battlezone[] battlezones;

    // Start is called before the first frame update
    void Start()
    {
        battlezones = new Battlezone[numberOfZones];
        for(int i = 0; i < numberOfZones; i++)
        {
            Battlezone zone = Instantiate(battleZonePrefab);
            Debug.Log("Render Size : " + zone.GetComponent<Renderer>().bounds.size.z);
            float offset = i * zone.GetComponent<Renderer>().bounds.size.z;
            zone.transform.parent = this.gameObject.transform;
            zone.transform.position += new Vector3(0, 0, offset);
            zone.name = "Battlezone " + i.ToString(); 
            battlezones[i] = zone;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
