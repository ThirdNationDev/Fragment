using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class Battlezone : MonoBehaviour
{
    public Battlefield battlefield;
    public GameObject battleZoneTilePrefab;

    public GameObject combatantPrefab;
    public List<Combatant> combatants;

    public int minTileNum;

    public int zoneNumber;
    private List<GameObject> tileArray;

    public float length
    {
        get
        {
            return battleZoneTilePrefab.transform.localScale.z;
        }
        private set { }
    }

    public Battlezone nextzone
    {
        get
        {
            return battlefield.getZone(zoneNumber + 1);
        }

        private set { }
    }

    public int numTiles
    {
        get
        {
            if (tileArray != null)
            {
                return tileArray.Count;
            }
            else { return 0; }
        }
        private set { }
    }

    public Battlezone prevzone
    {
        get
        {
            return battlefield.getZone(zoneNumber - 1);
        }

        private set { }
    }

    public float width
    {
        get
        {
            return numTiles * battleZoneTilePrefab.transform.localScale.x;
        }
        private set { }
    }

    public void AddCombatant(Combatant combatant)
    {
        combatants.Add(combatant);
        combatant.battlezone = this;
        if (combatants.Count > (numTiles / 2 + 1))
        {
            Resize(numTiles + 2);
            battlefield.ResizeBattlefield();
        }

        ArrangeCombatants();
    }

    /// <summary>
    /// Removes the gameobject from the tile and form the list of combatants,
    /// but does not destroy it.
    /// </summary>
    /// <param name="go"></param>
    public void RemoveCombatant(Combatant combatant)
    {
        combatants.Remove(combatant);
        combatant.transform.parent = null;
        if (combatants.Count <= (numTiles / 2 + 1))
        {
            Resize(numTiles - 2);
            battlefield.ResizeBattlefield();
        }
        ArrangeCombatants();
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

        if ((newTileNum / 2 + 1) < combatants.Count) //don't make it too small for the combatants
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

    public override string ToString()
    {
        return this.name;
    }

    /// <summary>
    /// Arranges the combatants evenly and centered across the zone.
    /// Not sure if stable.
    /// </summary>
    private void ArrangeCombatants()
    {
        if (combatants.Count == 0) { return; }  //if no combatants, no need to arrange

        int index = numTiles / 2 - (combatants.Count - 1);

        foreach (Combatant combatant in combatants)
        {
            combatant.transform.position = tileArray[index].transform.position;
            combatant.transform.parent = tileArray[index].transform;
            index += 2;
        }
    }

    private void Awake()
    {
        tileArray = new List<GameObject>();
        combatants = new List<Combatant>();
        Resize(minTileNum);
    }

    /// <summary>
    /// Destroys the gameobject tile. Does not destroy any child Combatants,
    /// but will remove them as child.
    /// </summary>
    /// <param name="tile"></param>
    private void DestroyTile(GameObject tile)
    {
        Combatant combatant = tile.GetComponentInChildren<Combatant>();
        if (combatant != null) //don't destroy combatant
        {
            combatant.transform.parent = null;
            ArrangeCombatants();
        }
        tileArray.Remove(tile);
        Destroy(tile);
    }

    /// <summary>
    /// Recenters the tiles so that within the battlezone object, the central tile  is at origin.
    /// </summary>
    private void Recenter()
    {
        if (numTiles <= 0) { return; }

        int centerTileIndex = numTiles / 2; //works because always odd number of tiles, and first tileindex is 0
        Vector3 center = tileArray[centerTileIndex].transform.position - this.gameObject.transform.position;

        for (int i = 0; i < numTiles; i++)
        {
            tileArray[i].transform.position -= center;
        }
    }
}