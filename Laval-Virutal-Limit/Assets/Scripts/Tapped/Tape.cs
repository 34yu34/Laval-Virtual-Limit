using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Throwable))]
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

    public enum TapingState
    {
        Stoped,
        Taping
    }

    private void FixedUpdate()
    {
        if (_current_tape_line != null && start_point != null)
        {
            reset_line_position();
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

        reset_line_position();
    }

    private void reset_line_position()
    {
        _current_tape_line.SetPosition(0, transform.position);
        _current_tape_line.SetPosition(1, start_point.transform.position);
    }

    private void finish_taping(Box box)
    {
        Destroy(_current_tape_line.gameObject);
        _current_tape_line = null;
        start_point = null;

        _state = TapingState.Stoped;

        box.ChangeStateTaped();
    }
}
