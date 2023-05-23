using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BattleZone2 : MonoBehaviour
{
    public GameObject battleZoneTilePrefab;

    public int numCombatants;

    public List<GameObject> combatants;

    public int minWidth;

    List<GameObject> tileArray;

    public TextMeshProUGUI inputWidth;

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

    //only odd widths allowed, will increase by one if even
    public void Resize(int newWidth)
    {
        if(newWidth%2 == 0)
        {
            newWidth++;
        }
        Vector3 scale = battleZoneTilePrefab.transform.localScale;

        if(newWidth == width || newWidth < 0)
        {
            return;
        }
        else if(newWidth > width)
        {

            //begin adding new tiles after existing ones
            for (int x = width; x < newWidth; x++)
            {

                GameObject tile = Instantiate(battleZoneTilePrefab);
                tile.transform.position += Vector3.Scale(scale, new Vector3(x, 0, 0));
                tile.transform.parent = this.transform;
                tile.name = "Tile " + x.ToString();
                tileArray.Add(tile);
            }
        }
        else //newWidth is smaller, must remove cells
        {
            for(int x = width-1; x > newWidth; x--){
                GameObject deadTile = tileArray[x];
                tileArray.RemoveAt(x);
                Destroy(deadTile);

            }
            

        }


    }    

    public void ResizeInput()
    {
        Debug.Log("Input: " + inputWidth.text.Trim());
        string input = inputWidth.text.Trim();
        Resize(int.Parse(input));
        //Resize(int.Parse("7"));
        //Resize(Convert.ToInt32(input));
    }

    public void AddCombatant()
    {
        //Check to see if there is sufficient space. If not, add more tiles.
        //Each combatant should have an empty tile between it and the closest one
        numCombatants++;
        if(tileArray.Count < (numCombatants*2 + 1))
        {
            Resize(numCombatants);
        }
        GameObject combatant = Instantiate(combatantPrefab);
        combatant.transform.position = tileArray[0].transform.position;
        combatant.transform.parent = tileArray[0].transform;

    }
}
