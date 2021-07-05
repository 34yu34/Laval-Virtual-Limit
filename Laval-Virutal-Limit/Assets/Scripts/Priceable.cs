using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priceable : MonoBehaviour
{
    [SerializeField]
    private int _spawn_price;

    public int SpawnPrice => _spawn_price;

    [SerializeField]
    private int _sell_price;

    public int SellPrice => _sell_price;
}
