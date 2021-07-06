using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TapedPoint : MonoBehaviour
{
    private SphereCollider _collider;
    private SphereCollider Collider => _collider ??= GetComponent<SphereCollider>();

    private Box _box;
    public Box Box => _box ??= GetComponentInParent<Box>();

    private void Start()
    {
        Collider.isTrigger = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        var tape = other.GetComponent<Tape>();

        tape?.TapedContact(this);

    }

    public TapedPoint GetPeer()
    {
        return Box.OtherPoint(this);
    }

}
