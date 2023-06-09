using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class Battlezone2 : MonoBehaviour
{
    public GameObject battleZoneTilePrefab;

    public List<GameObject> combatants;

    public int minWidth;

    List<GameObject> tileArray;

    public TMP_InputField inputWidth;

    public int zoneNumber;

    public int width { get {
            if (tileArray != null)
            {
                return tileArray.Count;
            }
            else { return 0; }
        } private set { } }

    public GameObject combatantPrefab;

    void Awake()
    {
        tileArray = new List<GameObject>();
        combatants = new List<GameObject>();
        Resize(minWidth);
    }

    /// <summary>
    /// Change the number of tiles to newWidth.
    /// Removed tiles are destroyed.
    /// Recenter() is called at the end.
    /// </summary>
    /// <param name="newWidth">Must be odd. Value will be incremented if even.</param>
    public void Resize(int newWidth)
    {

        //input checks
        if (newWidth % 2 == 0)  //odd numbers only
        {
            newWidth++;
        }

        if (newWidth == width || newWidth < 0) //don't do unecessary work
        {
            return;
        }

        if((newWidth/2 +1) < combatants.Count) //don't make it too small for the combatants
        {
            return;
        }

        if (newWidth < width)  //destroy extra tiles
        {
            for (int x = width - 1; x >= newWidth; x--)
            {
                DestroyTile(tileArray[x]);
            }
        }
        else if (newWidth > width) //create new tiles on the end
        {
            Vector3 lastTilePos;
            if (width > 0)
            {
                lastTilePos = tileArray[width - 1].transform.position;
            }
            else
            {
                lastTilePos = Vector3.zero;
            }

            for (int i = width; i < newWidth; i++)
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
    }

    /// <summary>
    /// Recenters the tiles so that within the battlezone object, the central tile  is at origin.
    /// </summary>
    void Recenter()
    {
        if(width <= 0) { return; }

        int centerTileIndex = width / 2; //works because always odd number of tiles, and first tileindex is 0
        Vector3 center = tileArray[centerTileIndex].transform.position;

        for(int i = 0; i < width; i++)
        {
            tileArray[i].transform.position -= center;
        }

    }


    /// <summary>
    /// For resizing zone with UI command
    /// </summary>
    public void ResizeInput()
    {
        string input = inputWidth.text.Trim();
        Resize(int.Parse(input));

    }

    /// <summary>
    /// Adds a prefab combatant, then rearranges the zone
    /// </summary>
    public void AddCombatant()
    {
        GameObject combatant = Instantiate(combatantPrefab);
        combatants.Add(combatant);
        if(combatants.Count > (width / 2 +1))
        {
            Resize(width += 2);
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

        int index = tileArray.Count/2 - (combatants.Count - 1);
        
        foreach(GameObject go in combatants)
        {
            go.transform.position = tileArray[index].transform.position;
            go.transform.parent = tileArray[index].transform;
            index += 2;
        }

    }
}
