using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Duplicator : MonoBehaviour
{

    [SerializeField]
    private float delay_to_spawn = 1f;
    [SerializeField]
    private GameObject _to_spawn;

    private Renderer[] renderer_list;
    private Rigidbody _rb;
    private Vector3 init_pos;
    private Quaternion init_rot;
    private int n_colliders_in = 0;
    private float time_to_render;

    public void Start()
    {
        disable_colliders();
        setup_initial_transform();
        setup_rb();
        renderer_list = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        if(Time.time > time_to_render)
        {
            foreach(var renderer in renderer_list)
            {
                renderer.enabled = true;
            }
        }

        if(n_colliders_in > 0)
        {
            time_to_render = Time.time + delay_to_spawn;
        }
         
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
        foreach(var renderer in renderer_list)
        {
            renderer.enabled = false;
        }
        time_to_render = Time.time + delay_to_spawn;
    }

    private void set_init_params(GameObject next_duplicator)
    {
        next_duplicator.name = gameObject.name;
        next_duplicator.transform.position = init_pos;
        next_duplicator.transform.rotation = init_rot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<HandCollider>() == null && other.GetComponentInParent<Box>() == null)
        {
            return;
        }
        n_colliders_in++;
        if( n_colliders_in != 1 || other.GetComponentInParent<Box>() != null)
        {
            return;
        }
        spawn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<HandCollider>() == null && other.GetComponentInParent<Box>() == null)
        {
            return;
        }
        n_colliders_in--;
    }

}
