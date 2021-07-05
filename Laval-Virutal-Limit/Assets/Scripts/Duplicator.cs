using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Duplicator : MonoBehaviour
{
    [SerializeField]
    private GameObject _to_spawn;
    private Rigidbody _rb;
    private Vector3 init_pos;
    private Quaternion init_rot;
    private int n_colliders_in = 0;

    public void Start()
    {
        disable_colliders();
        setup_initial_transform();
        setup_rb();
    }

    private void disable_colliders()
    {
        var collider_list = GetComponents<Collider>();
        foreach (var collider in collider_list)
        {
            collider.isTrigger = true;
        }
    }

    private void setup_rb()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    private void setup_initial_transform()
    {
        init_pos = transform.position;
        init_rot = transform.rotation;
    }

    private void spawn()
    {
        var spawned_item = Instantiate<GameObject>(_to_spawn);
        set_init_params(spawned_item);
    }

    private void set_init_params(GameObject next_duplicator)
    {
        next_duplicator.name = gameObject.name;
        next_duplicator.transform.position = init_pos;
        next_duplicator.transform.rotation = init_rot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<HandCollider>() == null)
        {
            return;
        }
        n_colliders_in++;
        if( n_colliders_in != 1)
        {
            return;
        }
        spawn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<HandCollider>() == null)
        {
            return;
        }
        n_colliders_in--;
    }

}
