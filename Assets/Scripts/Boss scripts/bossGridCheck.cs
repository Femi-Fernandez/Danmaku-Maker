﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bossGridCheck : MonoBehaviour
{
    public Tilemap bossTileMap;
    public RuleTile smartTile;
    public bool touched;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        //Debug.Log(collision.transform.position);
        if (collision.gameObject.tag == "boss grid")
        {
        Debug.Log(collision.gameObject.tag);
        Debug.Log(transform.position);
            Vector3Int currentCells = bossTileMap.WorldToCell(transform.position);
            touched = true;
        bossTileMap.SetTile(currentCells, smartTile);

        }

    }


}


