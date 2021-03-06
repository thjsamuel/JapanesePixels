﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ObjectStats {
    TimerRoutine explode_time;
    bool hasExploded;
    bool hasMoved;
    public int effectRange = 4;
    Vector3Int startPos;
    Vector3Int[] effectPos;
    public bool unstoppable = false;
    public bool playsSound = false;
	// Use this for initialization
	void Start () {
        explode_time = gameObject.AddComponent<TimerRoutine>();
        explode_time.initTimer(3);
        explode_time.executedFunction = Explode;
        hasExploded = false;
        effectPos = new Vector3Int[effectRange * 4 + 1];
        hasMoved = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (hasExploded == false)
        {
            explode_time.executeFunction();
            hasExploded = true;
        }
        startPos = GameModeManager.instance.gameGrid.GetWorldFlToCellPos(transform.position);
    }

    void Explode()
    {
        if (playsSound)
            GameModeManager.instance.GetComponent<AudioPlayer>().PlayOnceTrack(2, 1);
        if (unstoppable)
        {
            spawnUnstopFlames();
        }
        else
            spawnFlames();
        Destroy(this.gameObject);
    }

    void spawnFlames()
    {
        for (int x = 0; x <= effectRange; ++x)
        {
            Vector3Int spawnCell = Vector3Int.zero;
            spawnCell.Set(startPos.x + x, startPos.y, startPos.z);
            if (TileRefManager.instance.GetTileAtCellPos(TileRefManager.TILEMAP_TYPE.TILEMAP_SOLIDWALL, spawnCell))
            {
                break;
            }
            Vector3 spawnPos = GameModeManager.instance.gameGrid.GetCellMiddleWPOS(spawnCell);
            GameObject flame = GameObject.Instantiate(EnemyMoveController.instance.enemyPrefabs[2], spawnPos, transform.localRotation);
            flame.GetComponent<ObjectStats>().damage = damage; // damage
            flame.SetActive(true);
            // Allow spawning of flame at the destructible block position before stopping all spawns
            if (TileRefManager.instance.GetTileAtCellPos(TileRefManager.TILEMAP_TYPE.TILEMAP_DESTRUCTIBLE, spawnCell))
                break;
        }

        for (int x = -1; x >= -effectRange; --x)
        {
            Vector3Int spawnCell = Vector3Int.zero;
            spawnCell.Set(startPos.x + x, startPos.y, startPos.z);
            if (TileRefManager.instance.GetTileAtCellPos(TileRefManager.TILEMAP_TYPE.TILEMAP_SOLIDWALL, spawnCell))
            {
                break;
            }
            Vector3 spawnPos = GameModeManager.instance.gameGrid.GetCellMiddleWPOS(spawnCell);
            GameObject flame = GameObject.Instantiate(EnemyMoveController.instance.enemyPrefabs[2], spawnPos, transform.localRotation);
            flame.GetComponent<ObjectStats>().damage = damage;
            flame.SetActive(true);
            if (TileRefManager.instance.GetTileAtCellPos(TileRefManager.TILEMAP_TYPE.TILEMAP_DESTRUCTIBLE, spawnCell))
                break;
        }

        for (int y = 0; y <= effectRange; ++y)
        {
            Vector3Int spawnCell = Vector3Int.zero;
            spawnCell.Set(startPos.x, startPos.y + y, startPos.z);
            if (TileRefManager.instance.GetTileAtCellPos(TileRefManager.TILEMAP_TYPE.TILEMAP_SOLIDWALL, spawnCell))
            {
                break;
            }
            Vector3 spawnPos = GameModeManager.instance.gameGrid.GetCellMiddleWPOS(spawnCell);
            GameObject flame = GameObject.Instantiate(EnemyMoveController.instance.enemyPrefabs[2], spawnPos, transform.localRotation);
            flame.GetComponent<ObjectStats>().damage = damage;
            flame.SetActive(true);
            if (TileRefManager.instance.GetTileAtCellPos(TileRefManager.TILEMAP_TYPE.TILEMAP_DESTRUCTIBLE, spawnCell))
                break;
        }

        for (int y = -1; y >= -effectRange; --y)
        {
            Vector3Int spawnCell = Vector3Int.zero;
            spawnCell.Set(startPos.x, startPos.y + y, startPos.z);
            if (TileRefManager.instance.GetTileAtCellPos(TileRefManager.TILEMAP_TYPE.TILEMAP_SOLIDWALL, spawnCell))
                break;
            Vector3 spawnPos = GameModeManager.instance.gameGrid.GetCellMiddleWPOS(spawnCell);
            GameObject flame = GameObject.Instantiate(EnemyMoveController.instance.enemyPrefabs[2], spawnPos, transform.localRotation);
            flame.GetComponent<ObjectStats>().damage = damage;
            flame.SetActive(true);
            if (TileRefManager.instance.GetTileAtCellPos(TileRefManager.TILEMAP_TYPE.TILEMAP_DESTRUCTIBLE, spawnCell))
                break;
        }
    }

    void spawnUnstopFlames()
    {
        for (int x = -effectRange; x <= effectRange; ++x)
        {
            for (int y = -effectRange; y <= effectRange; ++y)
            {
                Vector3Int spawnCell = Vector3Int.zero;
                spawnCell.Set(startPos.x + x, startPos.y + y, startPos.z);
                Vector3 spawnPos = GameModeManager.instance.gameGrid.GetCellMiddleWPOS(spawnCell);
                GameObject flame = GameObject.Instantiate(EnemyMoveController.instance.enemyPrefabs[2], spawnPos, transform.localRotation);
                flame.GetComponent<ObjectStats>().damage = damage; // damage
                flame.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if  ((obj.gameObject.layer == 14 || obj.gameObject.layer == 15) && hasMoved )
            GetComponent<Collider2D>().isTrigger = false;
        //else
          //  hasMoved = false;
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        hasMoved = true;
    }
}
