using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rotator))]
public class ConveyorSpawner : MonoBehaviour
{
    [SerializeField]
    private ConveyorPad _pad;

    [SerializeField]
    private float _time_between_spawn;

    [SerializeField]
    private float _speed;

    private Rotator _rotator;

    public Rotator Rotator => _rotator ??= GetComponent<Rotator>();

    private float _next_spawn_timestamp;

    private void Start()
    {
        _next_spawn_timestamp = 0;
    }

    private void FixedUpdate()
    {
        if (_next_spawn_timestamp < Time.fixedTime)
        {
            SpawnPad();
            _next_spawn_timestamp = Time.fixedTime + _time_between_spawn;
        }
    }

    private void SpawnPad()
    {
        ConveyorPad pad = Instantiate(_pad);

        pad.Speed = _speed;

        pad.transform.position = transform.position;

        pad.SetNextDestination(Rotator.Destination);
    }
}
