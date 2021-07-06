using System;
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

    [SerializeField]
    private Destroyer _destroyer;

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
        _load_start_informations();
    }

    private void _load_start_informations()
    {
        if (GameInfoManager.Instance == null)
        {
            return;
        }

        _delay_at_the_end = GameInfoManager.Instance._end_delay;
        _start_delay = GameInfoManager.Instance._start_delay;
        _wrappable_to_spawn = GameInfoManager.Instance._spawn_items_list;
        _time_between_object =  GameInfoManager.Instance._time_between_items;
        _spawner.Speed = GameInfoManager.Instance._belt_speed;
<<<<<<< HEAD
=======
        Destroy(GameInfoManager.Instance.gameObject);
>>>>>>> lobby
    }

    private void FixedUpdate()
    {
        if (_start_timestamp > Time.fixedTime)
        {
            return;
        }

        check_object_to_spawn();

        if (_last_object_spawn_timestamp != 0)
        {
            check_game_timer();
            check_all_object_destroyed();
        }
    }

    private void check_all_object_destroyed()
    {
        if (_destroyer.DestroyedItemCount == _wrappable_to_spawn.Length)
        {
            change_scene();
        }
    }

    private void check_object_to_spawn()
    {
        if (_next_object_spawn_timestamp < Time.fixedTime && _last_object_spawn_timestamp == 0)
        {
            _next_object_spawn_timestamp = _time_between_object + Time.fixedTime;

            select_next_item();
        }
    }

    private void check_game_timer()
    {
        if (Time.fixedTime - _last_object_spawn_timestamp > _delay_at_the_end)
        {
            change_scene();
        }
    }

    private void change_scene()
    {
        SceneManager.LoadScene(_next_scene);
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
