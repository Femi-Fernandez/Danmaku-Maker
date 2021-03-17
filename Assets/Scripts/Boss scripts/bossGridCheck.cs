using System.Collections;
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
        if (collision.gameObject.tag == "boss grid")
        {
            if (!touched)
            {
                placeTile();
            }
        }
    }

    public void placeTile()
    {
        Vector3Int currentCells = bossTileMap.WorldToCell(transform.position);

        touched = true;
        bossTileMap.SetTile(currentCells, smartTile);
    }
    
    public void removeTile()
    {
            Vector3Int currentCells = bossTileMap.WorldToCell(transform.position);
            touched = false;
            bossTileMap.SetTile(currentCells, null);
    }

    public void removeTurretTile(Transform turretLoc)
    {
        Vector3Int currentCells = bossTileMap.WorldToCell(turretLoc.position);
        touched = false;
        bossTileMap.SetTile(currentCells, null);
    }
}


