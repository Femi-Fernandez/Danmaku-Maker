﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class turretDrop : MonoBehaviour, IDropHandler
{
    public GameObject turret;
    public GameObject boss;
    public int numOfTurrets = 0;
    int TotalNumberOfTurrets;
    public RectTransform restrictPlace;

    GameObject[] turrets;
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform turretPanel = transform as RectTransform;
        //throw new System.NotImplementedException();
        if (!RectTransformUtility.RectangleContainsScreenPoint(turretPanel, Input.mousePosition) ||
            !RectTransformUtility.RectangleContainsScreenPoint(restrictPlace, Input.mousePosition))
        {
            //Debug.Log("object dragged from panel!");

            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject spawnedTurret = Instantiate(turret, worldPosition, transform.rotation) as GameObject;
            spawnedTurret.name = "turret " + numOfTurrets;
            var currentPos = spawnedTurret.transform.position;
           // Debug.Log(currentPos.x + ", " + currentPos.y + ", " + currentPos.z);
            spawnedTurret.transform.position = new Vector3(  (Mathf.Round(currentPos.x)),
                                                             (Mathf.Round(currentPos.y)),
                                                             (Mathf.Round(currentPos.z)  ));
            //numOfTurrets++;



            turrets = GameObject.FindGameObjectsWithTag("turret Main");

            if (turrets.Length ==1)
            {
                TotalNumberOfTurrets = 0;
            }

            spawnedTurret.transform.position = checkSurroundings(spawnedTurret);

            if (spawnedTurret.transform.position.x == .123f)
            {
                Destroy(spawnedTurret);
                return;
            }
            spawnedTurret.transform.parent = boss.transform;
            SetDefaultValues(spawnedTurret);

            TotalNumberOfTurrets++;

            for (int i = 0; i < turrets.Length; i++)
            {   
                turrets[i].GetComponent<turretSubwaveStorage>().TotalNumberOfTurrets = TotalNumberOfTurrets;
            }
        }       
    }
    public static float RoundToNearestHalfSafer(float a)
    {
        return a = a - (a % 0.5f);
    }

    void SetDefaultValues(GameObject turret)
    {
        for (int i = 0; i < 4; i++)
        {
            Turret childToSet = turret.transform.GetChild(i).gameObject.GetComponent<Turret>();
            childToSet.turretLocation.x = turret.transform.localPosition.x;
            childToSet.turretLocation.y = turret.transform.localPosition.y;
            childToSet.parentTurret = transform.name;
            childToSet.numberActiveStreams = 1;

            childToSet.rotateAngleDirection = 90;
            childToSet.rotateAngleWidth = 10;
            childToSet.rotateSpeed = 5;
            childToSet.bulletBaseSpeed = 3;
            childToSet.numOfBullets = 4;
            childToSet.firerate = 1;
            childToSet.angleBetweenBullets = 10;
            //childToSet.turretLocation = transform.position;
        }
    }
   
    bool[] areSurroundingsFull = new bool[9];

    Vector3 checkSurroundings(GameObject turretPlaceAttempt)
    {
        Vector3 t = turretPlaceAttempt.transform.position;
        for (int i = 0; i < 8; i++)
        {
            areSurroundingsFull[i] = false;
        }

        areSurroundingsFull[8] = checkTurrets(t, turretPlaceAttempt);
        if (areSurroundingsFull[8] == true)
            return t;


        t.x++;
      
        areSurroundingsFull[0] = checkTurrets(t, turretPlaceAttempt);
        if (areSurroundingsFull[0])
            return t;


        t.y--;
        areSurroundingsFull[1] = checkTurrets(t, turretPlaceAttempt);
        if (areSurroundingsFull[1])
            return t;


        t.x--;
        areSurroundingsFull[2] = checkTurrets(t, turretPlaceAttempt);
        if (areSurroundingsFull[2])
            return t;

        t.x--;
        areSurroundingsFull[3] = checkTurrets(t, turretPlaceAttempt);
        if (areSurroundingsFull[3])
            return t;

      
        t.y++;    
        areSurroundingsFull[4] = checkTurrets(t, turretPlaceAttempt);
        if (areSurroundingsFull[4])
            return t;


        t.y++;
        areSurroundingsFull[5] = checkTurrets(t, turretPlaceAttempt);
        if (areSurroundingsFull[5])
            return t;


        t.x++;
        areSurroundingsFull[6] = checkTurrets(t, turretPlaceAttempt);
        if (areSurroundingsFull[6])
            return t;


        t.x++;
        areSurroundingsFull[7] = checkTurrets(t, turretPlaceAttempt);
        if (areSurroundingsFull[7])
            return t;

        t.x = .123f;
        return t;
        //condition ? expressionIfTrue : expressionIfFalse
    }

    bool checkTurrets( Vector3 t, GameObject placedTur)
    {
        if (turrets != null)
        {
            for (int i = 0; i < turrets.Length; i++)
            {
                
                if (turrets[i].transform.position == t && placedTur != turrets[i])
                {
                    
                    return false;
                }
            }
        }
       
        return true;
    }
}
