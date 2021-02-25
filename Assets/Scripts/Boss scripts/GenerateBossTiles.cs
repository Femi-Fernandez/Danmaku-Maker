using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateBossTiles : MonoBehaviour
{

    public GameObject[] turretLocations;
    public Tilemap bossTileMap;
    public RuleTile smartTile;

    public List<Vector2> linePoints = new List<Vector2>();
    public EdgeCollider2D lineToCore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turretLocations = GameObject.FindGameObjectsWithTag("turret Main");

        for (int i = 0; i < turretLocations.Length; i++)
        {
            if (linePoints != null)
            {
                linePoints.Clear();
            }
           
            linePoints.Add(new Vector2(0,0));

            linePoints.Add(new Vector2(turretLocations[i].transform.position.x, turretLocations[i].transform.position.y -3));

            //Debug.Log(turretLocations[i].transform.position.x + " " + turretLocations[i].transform.position.y);

            //lineToCore[i] = new EdgeCollider2D();

            lineToCore.points = linePoints.ToArray();

            Vector3Int currentCells = bossTileMap.WorldToCell(turretLocations[i].transform.position);

            //Debug.Log(currentCells);
            bossTileMap.SetTile(currentCells, smartTile);
        }

    }
}
