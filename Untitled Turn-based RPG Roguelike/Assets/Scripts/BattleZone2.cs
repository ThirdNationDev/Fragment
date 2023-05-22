using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZone2 : MonoBehaviour
{
    public GameObject battleZoneTilePrefab;

    public int numCombatants;

    GameObject[] tileArray;

    public GameObject combatantPrefab;

    void Awake()
    {
        Resize(numCombatants);
    }

    public void Resize(int numCombatants)
    {
        this.numCombatants = numCombatants;
        Vector3 scale = battleZoneTilePrefab.transform.localScale;

        if(numCombatants == 0)
        {
            tileArray = new GameObject[3];

        }
        else
        {
            tileArray = new GameObject[numCombatants*2 + 1];
        }

        for (int x = 0;x < tileArray.GetLength(0); x++)
        {
            
            GameObject tile = Instantiate(battleZoneTilePrefab);
            tile.transform.position += Vector3.Scale(scale, new Vector3(x, 0, 0));
            tile.transform.parent = this.transform;
            tile.name = "Tile " + x.ToString();
            tileArray[x] = tile;
            
        }
    }    

    public void AddCombatant()
    {
        //Check to see if there is sufficient space. If not, add more tiles.
        //Each combatant should have an empty tile between it and the closest one
        numCombatants++;
        if(tileArray.Length < (numCombatants*2 + 1))
        {
            Resize(numCombatants);
        }
        GameObject combatant = Instantiate(combatantPrefab);
        combatant.transform.position = tileArray[0].transform.position;
        combatant.transform.parent = tileArray[0].transform;

    }
}
