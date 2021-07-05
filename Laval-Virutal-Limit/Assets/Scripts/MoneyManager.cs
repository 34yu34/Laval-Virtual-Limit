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

    [SerializeField]
    private TextMesh text;

    private int _bank = 0;

    private int bank
    {
        get => _bank;
        set
        {
            _bank = value;
            on_change();
        }
    }

    private void on_change()
    {
        text.text = $"account balance:\n{bank}€";
    }

    public void Pay(int cost)
    {
        bank -= cost;
    }

    public void Sell(int price)
    {
        bank += price;
    }

}
