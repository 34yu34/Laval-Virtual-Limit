using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Sizeable))]
[RequireComponent(typeof(Priceable))]
public class Boxable : MonoBehaviour
{

    private Sizeable _sizeable;
    public Sizeable Sizeable => _sizeable ??= GetComponent<Sizeable>();
    private Priceable _priceable;

    public Priceable Priceable => _priceable ??= GetComponent<Priceable>();

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().isTrigger = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
