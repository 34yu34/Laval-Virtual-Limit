using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{

    private static MoneyManager __instance;

    public static MoneyManager Instance
    {
        get
        {
            if (__instance == null)
            {
                __instance = FindObjectOfType<MoneyManager>();

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
        DontDestroyOnLoad(Instance);
    }

    private int _entity_count = 0;
    private int _note_sum = 0;

    public int Note => _note_sum / _entity_count;

    private int bank
    {
        get => _entity_count;
        set
        {
            _entity_count = value;
        }
    }

    public void Pay(int cost)
    {
        bank += cost;
    }

    public void Sell(int price)
    {
        _note_sum += price;
    }

}
