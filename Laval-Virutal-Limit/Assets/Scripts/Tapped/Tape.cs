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

    [SerializeField]
    private float _angle_to_touch = 2f;

    private LineRenderer _current_tape_line;

    private TapedPoint _start_point;

    private TapedPoint _end_point;

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
        if (_current_tape_line != null && _start_point != null)
        {
            reset_line_position();
            send_raycast();
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
            if (taped_point == _start_point || taped_point.Box != _start_point.Box)
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

        _start_point = at;
        _end_point = at.GetPeer();

        _state = TapingState.Taping;

        _tape_timestamp = Time.fixedTime + _max_tape_time;

        reset_line_position();

        audioData.Play();
    }

    private void reset_line_position()
    {
        _current_tape_line.SetPosition(0, transform.position);
        _current_tape_line.SetPosition(1, _start_point.transform.position);
    }

    private void send_raycast()
    {
        Vector3 tape_line = _start_point.transform.position - transform.position;

        Vector3 optimal_line = _start_point.transform.position - _end_point.transform.position;

        if (Vector3.Angle(tape_line, optimal_line) < _angle_to_touch && tape_line.magnitude > optimal_line.magnitude)
        {
            finish_taping(_start_point.Box);
        }

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
        _start_point = null;
        _end_point = null;

        _state = TapingState.Stoped;
    }
}
