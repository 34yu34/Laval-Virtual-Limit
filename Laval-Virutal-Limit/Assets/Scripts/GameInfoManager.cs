using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoManager : MonoBehaviour
{
    private static GameInfoManager __instance;

    public static GameInfoManager Instance
    {
        get
        {
            if (__instance == null)
            {
                __instance = FindObjectOfType<GameInfoManager>();

                if (__instance == null)
                {
                    Debug.LogError("there is an instance of money manager missing");
                }
            }

            return __instance;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public Wrappable[] _spawn_items_list;

    public float _start_delay;

    public float _end_delay;

    public float _time_between_items;

    public float _belt_speed;
}
