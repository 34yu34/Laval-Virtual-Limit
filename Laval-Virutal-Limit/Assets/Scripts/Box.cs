using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour
{
    private BoxState _state;
    public BoxState State => _state;

    [SerializeField]
    GameObject _opened_box_prefab;

    [SerializeField]
    GameObject _closed_box_prefab;

    [SerializeField]
    GameObject _taped_box_prefab;

    GameObject _current_box;

    // Start is called before the first frame update
    void Start()
    {
        _current_box = Instantiate(_opened_box_prefab);
        set_position_box();
        _state = BoxState.opened;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStateClosed()
    {
        Destroy(_current_box);
        _current_box = Instantiate(_closed_box_prefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        set_position_box();
        _state = BoxState.closed;
    }

    public void ChangeStateTaped()
    {
        Destroy(_current_box);
        _current_box = Instantiate(_taped_box_prefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        set_position_box();
        _state = BoxState.taped;
    }

    private void set_position_box()
    {
        _current_box.transform.parent = gameObject.transform;
        _current_box.transform.localPosition = Vector3.zero;
        _current_box.transform.localRotation = Quaternion.identity;
        _current_box.transform.localScale = Vector3.one;
    }
}
