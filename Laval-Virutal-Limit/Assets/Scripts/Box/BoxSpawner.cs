using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField]
    private Box to_spawn;
    private Box spawned = null;

    [SerializeField]
    private float distance_to_spawn = 1f;

    Vector3 init_pos;
    Quaternion init_rot;

    public void Start()
    {
        init_rot = transform.rotation;
        init_pos = transform.position;
    }

    private void FixedUpdate()
    {
        if(spawned == null || Vector3.Distance(spawned.transform.position, transform.position) > distance_to_spawn)
        {
            spawn();
        }
    }

    private void spawn()
    {
        spawned = Instantiate<Box>(to_spawn);
        spawned.transform.rotation = init_rot;
        spawned.transform.position = init_pos;
    }
}
