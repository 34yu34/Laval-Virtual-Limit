using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Sizeable))]
[RequireComponent(typeof(Priceable))]
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

    TapedPoint[] taped_points;

    GameObject _current_box;

    private Sizeable _sizeable;
    public Sizeable Sizeable => _sizeable ??= GetComponent<Sizeable>();

    private Priceable _priceable;

    public Priceable Priceable => _priceable ??= GetComponent<Priceable>();

    // Start is called before the first frame update
    void Start()
    {
        _current_box = Instantiate(_opened_box_prefab);
        taped_points = GetComponentsInChildren<TapedPoint>();

        SetPointActive(false);

        set_position_box();
        _state = BoxState.opened;
    }

    public void ChangeStateClosed()
    {
        Destroy(_current_box);
        _current_box = Instantiate(_closed_box_prefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);

        SetPointActive(true);

        set_position_box();
        _state = BoxState.closed;
    }


    public void ChangeStateTaped()
    {
        Destroy(_current_box);
        _current_box = Instantiate(_taped_box_prefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        SetPointActive(false);
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

    public TapedPoint OtherPoint(TapedPoint curr_points)
    {
        foreach (var pt in taped_points)
        {
            if (pt.Box == curr_points.Box && pt != curr_points)
            {
                return pt;
            }
        }

        return null;
    }

    private void SetPointActive(bool val)
    {
        foreach (var pt in taped_points)
        {
            pt.gameObject.SetActive(val);
        }
    }
}
