using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Battlefield : MonoBehaviour
{
    [SerializeField]
    private Battlezone battleZonePrefab;

    [SerializeField]
    private int initNumZones;

    private Battlezone[] battlezones;

    public int numZones
    {
        get
        {
            return battlezones.Length;
        }

        private set { }
    }

    private int zoneTileNumber;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    internal void CreateBattlefield()
    {
        if(BattleManager.Instance.playerStartingZone <0 
            || BattleManager.Instance.enemyStartingZone <0
            || BattleManager.Instance.playerStartingZone >= initNumZones
            || BattleManager.Instance.enemyStartingZone >= initNumZones)
        {
            Debug.LogError("Battlefield starting zone is incorrect.");
        }

        zoneTileNumber = battleZonePrefab.minTileNum;

        battlezones = new Battlezone[initNumZones];
        for (int i = 0; i < initNumZones; i++)
        {
            Battlezone zone = Instantiate(battleZonePrefab);
            //Debug.Log("Render Size : " + zone.GetComponent<Renderer>().bounds.size.z);
            float offset = i * zone.length;
            zone.transform.parent = this.gameObject.transform;
            zone.transform.position += new Vector3(0, 0, offset);
            zone.name = "Battlezone " + i.ToString();
            zone.zoneNumber = i;
            zone.battlefield = this;
            battlezones[i] = zone;
        }
    }

    public void PlaceCombatants(Combatant[] combatants)
    {
        Combatant c;
        int startingZone = 0;
        for(int i = 0; i < combatants.Length; i++)
        {
            c = combatants[i];
            if(c is PlayerCombatant)
            {
                startingZone = BattleManager.Instance.playerStartingZone;
            }
            else if ( c is EnemyCombatant)
            {
                startingZone = BattleManager.Instance.enemyStartingZone;
            }
            c.EnterCombat(battlezones[startingZone]);
        }
    }

    public Battlezone getZone(int i)
    {
        if ((0 <= i) &&(i < battlezones.Length))
        {
            return battlezones[i];
        }
        else
        {
            return null;
        }
    }

    public void ResizeBattlefield()
    {
        //find the max tile width across all the zones
        int maxTileWidth=0;
        int minZoneWidth;
        foreach(Battlezone zone in battlezones)
        {
            minZoneWidth = zone.combatants.Count + 2;
            maxTileWidth = minZoneWidth > maxTileWidth ? minZoneWidth : maxTileWidth;
        }

        foreach(Battlezone zone in battlezones)
        {
            zone.Resize(maxTileWidth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
