using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Belt : MonoBehaviour
{
    [SerializeField]
    private Wrappable[] _wrappable_to_spawn;

    [SerializeField]
    private ConveyorSpawner _spawner;

    private int _curr_index = 0;

    [SerializeField]
    private float _time_between_object;

    private float _next_object_spawn_timestamp;

    private float _last_object_spawn_timestamp = 0;

    [SerializeField]
    private float _delay_at_the_end;

    [SerializeField]
    private float _start_delay;

    private float _start_timestamp;

    [SerializeField]
    private string _next_scene;

    public int ItemsLeft => _wrappable_to_spawn.Length - _curr_index;

    private void Start()
    {
        _start_timestamp = _start_delay + Time.fixedTime;
    }

    private void FixedUpdate()
    {
        if (_start_timestamp > Time.fixedTime)
        {
            return;
        }

        if (_next_object_spawn_timestamp < Time.fixedTime && _last_object_spawn_timestamp == 0)
        {
            _next_object_spawn_timestamp = _time_between_object + Time.fixedTime;

            select_next_item();
        }
        if(_last_object_spawn_timestamp != 0)
        {
            check_game_timer();
        }
    }

    private void check_game_timer()
    {
        if (Time.fixedTime - _last_object_spawn_timestamp > _delay_at_the_end)
        {
            SceneManager.LoadScene(_next_scene);
        }
    }

    private void select_next_item()
    {
        _spawner.ItemToSpawn = _wrappable_to_spawn[_curr_index++ % _wrappable_to_spawn.Length];
        if (_curr_index >= _wrappable_to_spawn.Length)
        {
            _last_object_spawn_timestamp = Time.fixedTime;

        }

    }
}
