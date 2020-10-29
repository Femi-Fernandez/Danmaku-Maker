using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_pool : MonoBehaviour
{
    public static bullet_pool bulletPoolInstanse;

    [SerializeField]
    private GameObject pooledBullet;
    private bool notenoughBullets = true;

    public List<GameObject> bullets;

    private void Awake()
    {
        bulletPoolInstanse = this;
    }
    void Initialise()
    {
        bullets = new List<GameObject>();
    }
    public GameObject GetBullet()
    {

        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if (notenoughBullets)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }

        return null;
    }

  //  void Update()
  //  {
  //      
  //  }
}
