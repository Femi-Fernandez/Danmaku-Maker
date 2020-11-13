﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class turretDrop : MonoBehaviour, IDropHandler
{
    public GameObject turret;
    public GameObject boss;
    int numOfTurrets;
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform turretPanel = transform as RectTransform;
        //throw new System.NotImplementedException();
        if (!RectTransformUtility.RectangleContainsScreenPoint(turretPanel, Input.mousePosition))
        {
            Debug.Log("object dragged from panel!");

            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject spawnedTurret = Instantiate(turret, worldPosition, transform.rotation) as GameObject;
            spawnedTurret.name = "turret " + numOfTurrets;
            numOfTurrets++;

            spawnedTurret.transform.parent = boss.transform;
            spawnedTurret.GetComponent<Turret_Targeting>().enabled = false;
            spawnedTurret.GetComponent<Turret_Fire>().enabled = false;
            SetDefaultValues(spawnedTurret);
           // spawnedTurret.GetComponent<Turret>().rotateAngleDirection = 90;
        }
    }

    void SetDefaultValues(GameObject turret)
    {
        turret.GetComponent<Turret>().rotateAngleDirection = 90;
        turret.GetComponent<Turret>().rotateAngleWidth = 10;
        turret.GetComponent<Turret>().rotateSpeed = 5;
    }
}
