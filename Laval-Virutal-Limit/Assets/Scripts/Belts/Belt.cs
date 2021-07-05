using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField]
    private Wrappable[] _wrappable_to_spawn;

    [SerializeField]
    private ConveyorSpawner _spawner;

    [SerializeField]
    private float _time_between_object;

    private float _next_object_spawn_timestamp;


    private void FixedUpdate()
    {
        if (_next_object_spawn_timestamp < Time.fixedTime)
        {
            _next_object_spawn_timestamp = _time_between_object + Time.fixedTime;

            _spawner.ItemToSpawn = _wrappable_to_spawn[0];
        }
    }

}
