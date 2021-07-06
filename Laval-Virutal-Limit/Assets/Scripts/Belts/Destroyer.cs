using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Destroyer : BeltBehaviour
{
    private int _item_destoyed_count;
    public int DestroyedItemCount => _item_destoyed_count;

    private void Start()
    {
        StartBeltBehaviour();
        _item_destoyed_count = 0;
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
            if (pad.Link != null && pad.Link.Wrappable != null)
            {

                MoneyManager.Instance.Sell(pad.Link.Wrappable.Priceable.SellPrice);

                count_destroyed_object(pad);

                pad.Link.Wrappable.Kill();
                pad.Link.Disconnect();
            }
            Destroy(pad.gameObject);
        }
    }

    private void count_destroyed_object(ConveyorPad pad)
    {
        Box box = pad.Link.Wrappable.GetComponent<Box>();

        if (box == null || box.State != BoxState.opened)
        {
            _item_destoyed_count++;
        }
    }
}
