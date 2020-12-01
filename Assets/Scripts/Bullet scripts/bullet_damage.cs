using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_damage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("bullets hit");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<playerHealth>().Damage();
            //collision.transform.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

}
