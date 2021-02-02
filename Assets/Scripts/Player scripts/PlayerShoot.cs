using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject bullet;
    public float ShotDelay = .2f;
    private bool ReadyToShoot = true;

    public bool bulletCollidersOn;

    GameObject bul;

    public bool bullet_spin_left;
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Z) && ReadyToShoot)
        {
            bul = Instantiate(bullet, transform.position, transform.rotation);
            //bul.GetComponent<bullet_anim_controler>().spin_left = ReadyToShoot;
            bul.GetComponent<Animator>().SetBool("Spin_Left", bullet_spin_left);

            if (!bulletCollidersOn)
            {
                bul.GetComponent<Collider2D>().enabled = false;
            }

            ReadyToShoot = false;
            Invoke("ResetReadyToShoot", ShotDelay);
        }
	}
    void ResetReadyToShoot()
    {
        ReadyToShoot = true;
    }
}
