using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point_at_player : MonoBehaviour
{
    GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            Vector2 direction = _player.transform.position - transform.position;
            Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
            transform.rotation = rotation;
        }
        
    }
}
