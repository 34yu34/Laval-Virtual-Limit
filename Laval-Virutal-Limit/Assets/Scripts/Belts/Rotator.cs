using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Rotator : BeltBehaviour
{
    [SerializeField]
    private BeltBehaviour _destination;

    public Vector3 Destination => _destination.transform.position;

    private void Start()
    {
        StartBeltBehaviour();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ConveyorPad>()?.SetNextDestination(Destination);
    }
}
