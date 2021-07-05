using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BeltBehaviour : MonoBehaviour
{
    protected void StartBeltBehaviour()
    {
        setup_rigidbody();
    }

    private void setup_rigidbody()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;
    }
}
