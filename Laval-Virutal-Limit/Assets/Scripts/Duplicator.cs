using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// is throwable and frozen, but when taken, duplicate itself.
[RequireComponent(typeof(Valve.VR.InteractionSystem.Throwable))]
public class Duplicator : MonoBehaviour
{
    private Rigidbody _rb;
    private bool duplicated = false;
    private Vector3 init_pos;
    private Quaternion init_rot;

    public void Start()
    {
        init_pos = transform.position;
        init_rot = transform.rotation;
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    public void OnGrab()
    {
        if(duplicated)
        {
            return;
        }
        duplicate();
    }

    public void OnDrop()
    {
        allow_movement();
    }

    private void duplicate()
    {
        var next_duplicator = Instantiate<GameObject>(gameObject);
        set_init_params(next_duplicator);
        duplicated = true;
    }

    private void set_init_params(GameObject next_duplicator)
    {
        next_duplicator.name = gameObject.name;
        next_duplicator.transform.position = init_pos;
        next_duplicator.transform.rotation = init_rot;
    }

    private void allow_movement()
    {
        _rb.isKinematic = false;
    }
}
