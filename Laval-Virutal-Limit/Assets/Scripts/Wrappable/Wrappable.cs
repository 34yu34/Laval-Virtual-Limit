using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
[RequireComponent(typeof(Priceable))]
public class Wrappable : MonoBehaviour
{
    readonly float RELEASE_DISTANCE = 0.4f;

    public WrappableLink Link { get; set; }

    private Throwable _throwable;

    private Rigidbody _rigidbody;

    private Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

    private Priceable _priceable;

    public Priceable Priceable => _priceable ??= GetComponent<Priceable>();

    private bool _is_in_hand;

    private void Start()
    {
        _throwable = GetComponent<Throwable>();
        _throwable.onPickUp.AddListener(OnPickup);
        _throwable.onDetachFromHand.AddListener(OnDetachFromHand);
    }

    private void FixedUpdate()
    {
        move_item();

        if (Link != null && Vector3.Distance(transform.position, Link.Pad.transform.position) > RELEASE_DISTANCE)
        {
            Link.Disconnect();
        }
    }

    private void move_item()
    {
        if (Link != null && !_is_in_hand)
        {
            transform.position = Link.Pad.transform.position;
            transform.rotation = Quaternion.identity;
        }
    }

    public void OnPickup()
    {
        _is_in_hand = true;
    }

    public void OnDetachFromHand()
    {
        _is_in_hand = false;

        if (Link == null)
        {
            Rigidbody.isKinematic = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {

        ConveyorPad pad =  other.GetComponent<ConveyorPad>();

        if (pad != null)
        {
            AddOnBelt(pad);
        }
    }

    public void Kill()
    {
        if (!_is_in_hand)
        {
            Destroy(gameObject);
        }
    }

    public void AddOnBelt(ConveyorPad pad)
    {
        WrappableLink.Connect(this, pad);
        Rigidbody.isKinematic = true;
    }

    private void remove_from_belt()
    {
        Link.Disconnect();
        Rigidbody.isKinematic = false;
    }

}
