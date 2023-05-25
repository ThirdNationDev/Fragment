using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class Battlezone : MonoBehaviour
{
    public GameObject battleZoneTilePrefab;

    public List<GameObject> combatants;

    public int minTileNum;

    List<GameObject> tileArray;

    public int zoneNumber;

    public Battlefield battlefield;

    public int numTiles
    { 
        get
        {
            if (tileArray != null)
            {
                return tileArray.Count;
            }
            else { return 0; }
        } private set { }
    }

    public float width
    {
        get
        {
            return numTiles * battleZoneTilePrefab.transform.localScale.x;
        }
        private set { }
    }

    public float length
    {
        get
        {
            return battleZoneTilePrefab.transform.localScale.z;
        }
        private set { }
    }

    public GameObject combatantPrefab;

    void Awake()
    {
        tileArray = new List<GameObject>();
        combatants = new List<GameObject>();
        Resize(minTileNum);
    }

    /// <summary>
    /// Change the number of tiles to newTileNum.
    /// Removed tiles are destroyed.
    /// Recenter() is called at the end.
    /// </summary>
    /// <param name="newTileNum">Must be odd. Value will be incremented if even.</param>
    public void Resize(int newTileNum)
    {

        //input checks
        if (newTileNum % 2 == 0)  //odd numbers only
        {
            newTileNum++;
        }

        if (newTileNum == numTiles || newTileNum < 0) //don't do unecessary work
        {
            return;
        }

        if((newTileNum/2 +1) < combatants.Count) //don't make it too small for the combatants
        {
            return;
        }

        if (newTileNum < numTiles)  //destroy extra tiles
        {
            for (int x = numTiles - 1; x >= newTileNum; x--)
            {
                DestroyTile(tileArray[x]);
            }
        }
        else if (newTileNum > numTiles) //create new tiles on the end
        {
            Vector3 lastTilePos;
            if (numTiles > 0)
            {
                lastTilePos = tileArray[numTiles - 1].transform.position;
            }
            else
            {
                lastTilePos = Vector3.zero;
            }

            for (int i = numTiles; i < newTileNum; i++)
            {
                GameObject tile = Instantiate(battleZoneTilePrefab);
                tile.transform.parent = this.transform;
                tile.transform.position = lastTilePos + new Vector3(tile.transform.localScale.x, 0, 0);
                tile.name = "Tile " + i.ToString();
                tileArray.Add(tile);
                lastTilePos = tile.transform.position;

            }
        }
       Recenter();
       ArrangeCombatants();
    }    

    /// <summary>
    /// Destroys the gameobject tile. Does not destroy any child Combatants, 
    /// but will remove them as child.
    /// </summary>
    /// <param name="tile"></param>
    void DestroyTile(GameObject tile)
    {
        Combatant combatant = tile.GetComponentInChildren<Combatant>();
        if(combatant != null) //don't destroy combatant
        {
            combatant.transform.parent = null;
            ArrangeCombatants();
        }
        tileArray.Remove(tile);
        Destroy(tile);
    }

    /// <summary>
    /// Removes the gameobject from the tile and form the list of combatants,
    /// but does not destroy it.
    /// </summary>
    /// <param name="go"></param>
    public void RemoveCombatant(GameObject go)
    {
        combatants.Remove(go);
        go.transform.parent = null;
        if (combatants.Count <= (numTiles / 2 + 1))
        {
            Resize(numTiles - 2);
            battlefield.ResizeBattlefield();
        }
        ArrangeCombatants();
    }

    /// <summary>
    /// Recenters the tiles so that within the battlezone object, the central tile  is at origin.
    /// </summary>
    void Recenter()
    {
        if(numTiles <= 0) { return; }

        int centerTileIndex = numTiles / 2; //works because always odd number of tiles, and first tileindex is 0
        Vector3 center = tileArray[centerTileIndex].transform.position - this.gameObject.transform.position;

        for(int i = 0; i < numTiles; i++)
        {
            tileArray[i].transform.position -= center;
        }

    }


    /// <summary>
    /// Adds a prefab combatant, then rearranges the zone
    /// </summary>
    public void AddCombatant()
    {
        GameObject combatant = Instantiate(combatantPrefab);
        AddCombatant(combatant.GetComponent<Combatant>());

    }

    public void AddCombatant(Combatant combatant)
    {
        combatants.Add(combatant.gameObject);
        if (combatants.Count > (numTiles / 2 + 1))
        {
            Resize(numTiles+2);
            battlefield.ResizeBattlefield();
        }

        ArrangeCombatants();


    }
    /// <summary>
    /// Arranges the combatants evenly and centered across the zone.
    /// Not sure if stable. 
    /// </summary>
    void ArrangeCombatants()
    {
        if(combatants.Count == 0) { return; }  //if no combatants, no need to arrange

        int index = numTiles/2 - (combatants.Count - 1);
        
        foreach(GameObject go in combatants)
        {
            go.transform.position = tileArray[index].transform.position;
            go.transform.parent = tileArray[index].transform;
            index += 2;
        }

    }
}
