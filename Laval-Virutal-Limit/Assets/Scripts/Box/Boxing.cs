using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Boxing : MonoBehaviour
{
    private Box _box;
    private Boxable _current_boxable;
    private int n_colliders_in = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        _box = GetComponentInParent<Box>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_box.State != BoxState.opened)
            return;

        Boxable boxable = other.gameObject.GetComponent<Boxable>();
        if (boxable != null && boxable.Sizeable.Size <= _box.Sizeable.Size)
        {
            n_colliders_in++;
            _current_boxable = boxable;
            var boxable_throwable = other.gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>();
            boxable_throwable.onDetachFromHand.AddListener(OnDetach);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_box.State != BoxState.opened)
            return;

        Boxable boxable = other.gameObject.GetComponent<Boxable>();
        if (boxable != null)
        {
            if(--n_colliders_in > 0)
            {
                return;
            }
            _current_boxable = null;
            var boxable_throwable = other.gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>();
            boxable_throwable.onDetachFromHand.RemoveListener(OnDetach);

        }
    }

    private void OnDetach()
    {
        //put object in box and desactivate
        _current_boxable.gameObject.transform.parent = gameObject.transform;
        _current_boxable.gameObject.SetActive(false);

        _box.ChangeStateClosed();

        _box.Priceable.SetOpenBoxPrice(_current_boxable.Priceable.SpawnPrice);
    }
}
