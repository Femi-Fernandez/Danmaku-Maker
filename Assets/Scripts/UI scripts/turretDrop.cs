using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class turretDrop : MonoBehaviour, IDropHandler
{
    public GameObject turret;
    public GameObject boss;
    int numOfTurrets = 0;

    public RectTransform restrictPlace;

    GameObject[] turrets;
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform turretPanel = transform as RectTransform;
        //throw new System.NotImplementedException();
        if (!RectTransformUtility.RectangleContainsScreenPoint(turretPanel, Input.mousePosition) ||
            !RectTransformUtility.RectangleContainsScreenPoint(restrictPlace, Input.mousePosition))
        {
            Debug.Log("object dragged from panel!");

            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject spawnedTurret = Instantiate(turret, worldPosition, transform.rotation) as GameObject;
            spawnedTurret.name = "turret " + numOfTurrets;
            var currentPos = spawnedTurret.transform.position;
            spawnedTurret.transform.position = new Vector3((Mathf.Round(currentPos.x * 10)) / 10,
                                                           (Mathf.Round(currentPos.y * 10)) / 10,
                                                           (Mathf.Round(currentPos.z * 10)) / 10);
            //numOfTurrets++;

            spawnedTurret.transform.parent = boss.transform;

            SetDefaultValues(spawnedTurret);

            turrets = GameObject.FindGameObjectsWithTag("Turret");

            for (int i = 0; i < turrets.Length; i++)
            {
                turrets[i].GetComponent<Turret>().TotalNumberOfTurrets++;
            }
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
                //childToSet.turretLocation = transform.position;
            }
        }
    }
}
