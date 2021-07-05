using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Throwable))]
[RequireComponent(typeof(AudioSource))]
public class Tape : MonoBehaviour
{
    [SerializeField]
    private float _max_tape_time;

    [SerializeField]
    private LineRenderer _tape_line;

    private LineRenderer _current_tape_line;

    private TapedPoint start_point;

    private float _tape_timestamp;

    private TapingState _state;

    private Throwable _throwable;
    public Throwable Throwable => _throwable ??= GetComponent<Throwable>();

    private Rigidbody _rb;
    public Rigidbody Rb => _rb ??= GetComponent<Rigidbody>();

    public Vector3 initial_pos;
    public Quaternion intial_rotation;

    AudioSource audioData;

    public enum TapingState
    {
        Stoped,
        Taping
    }

    public void Start()
    {
        initial_pos = transform.position;
        intial_rotation = transform.rotation;

        Rb.constraints = RigidbodyConstraints.FreezeAll;
        Throwable.onDetachFromHand.AddListener(detach_from_hand);

        audioData = GetComponent<AudioSource>();
    }

    private void detach_from_hand()
    {
        transform.position = initial_pos;
        transform.rotation = intial_rotation;
    }

    private void FixedUpdate()
    {
        if (_current_tape_line != null && start_point != null)
        {
            reset_line_position();
        }

        if (_tape_timestamp < Time.fixedTime)
        {
            destroy_line();
        }
    }

    public void TapedContact(TapedPoint taped_point)
    {
        if (_state == TapingState.Stoped)
        {
            start_taping(taped_point);
        }

        if (_state == TapingState.Taping)
        {
            if (taped_point == start_point || taped_point.Box != start_point.Box)
            {
                return;
            }

            finish_taping(taped_point.Box);
        }
    }

    private void start_taping(TapedPoint at)
    {
        _current_tape_line = Instantiate(_tape_line);
        _current_tape_line.transform.position = Vector3.zero;

        start_point = at;

        _state = TapingState.Taping;

        _tape_timestamp = Time.fixedTime + _max_tape_time;

        reset_line_position();

        audioData.Play();
    }

    private void reset_line_position()
    {
        _current_tape_line.SetPosition(0, transform.position);
        _current_tape_line.SetPosition(1, start_point.transform.position);
    }

    private void finish_taping(Box box)
    {
        destroy_line();

        box.ChangeStateTaped();
        box.Priceable.SetTapedBoxPrice();
    }

    private void destroy_line()
    {
        if (_current_tape_line == null)
        {
            return;
        }

        Destroy(_current_tape_line.gameObject);
        _current_tape_line = null;
        start_point = null;

        _state = TapingState.Stoped;
    }
}
