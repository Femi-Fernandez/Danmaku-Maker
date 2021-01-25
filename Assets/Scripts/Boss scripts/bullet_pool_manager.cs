using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_pool_manager : MonoBehaviour
{

    public bullet_pool[] bullet_type;


    public GameObject GetBullet(int bulletType)
    {
        return bullet_type[bulletType].GetBullet();
    }
}
