using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Boxing : MonoBehaviour
{
    private Box _box;
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
        if (boxable != null)
        {
            //put object in box and desactivate
            boxable.gameObject.transform.parent = gameObject.transform;
            boxable.gameObject.SetActive(false);

            _box.ChangeStateClosed();
            
        }
    }
}
