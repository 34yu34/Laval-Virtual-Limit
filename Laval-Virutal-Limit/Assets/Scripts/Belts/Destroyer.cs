using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Destroyer : BeltBehaviour
{
    private void Start()
    {
        StartBeltBehaviour();
    }

    public void Kill(ConveyorPad pad)
    {
        Destroy(pad.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        ConveyorPad pad = other.GetComponent<ConveyorPad>();
        if (pad != null)
        {
            Destroy(pad.gameObject);
        }
    }

}
