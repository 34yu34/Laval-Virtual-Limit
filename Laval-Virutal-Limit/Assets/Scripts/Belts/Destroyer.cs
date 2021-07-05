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
            if (pad.Link != null)
            {
                MoneyManager.Instance.Sell(pad.Link.Wrappable.Priceable.SellPrice);

                pad.Link.Wrappable.Kill();
                pad.Link.Disconnect();
            }
            Destroy(pad.gameObject);
        }
    }
}
