using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject bullet;
    public float ShotDelay = .2f;
    private bool ReadyToShoot = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Z) && ReadyToShoot)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            ReadyToShoot = false;
            Invoke("ResetReadyToShoot", ShotDelay);
        }
	}
    void ResetReadyToShoot()
    {
        ReadyToShoot = true;
    }
}
