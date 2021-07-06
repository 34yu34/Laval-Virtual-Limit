using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priceable : MonoBehaviour
{
    [SerializeField]
    private float _untaped_box_rate = 0.75f;

    [SerializeField]
    private float _taped_box_profit_rate = 0.1f;

    [SerializeField]
    private int _spawn_price;

    public int SpawnPrice => _spawn_price;

    [SerializeField]
    private int _sell_price = 50;

    public int SellPrice => _sell_price;

    private bool _is_right = true;

    public void SetOpenBoxPrice(int item_price, bool has_right_box)
    {
        _sell_price = has_right_box ? 75 : 65;
        _is_right = has_right_box;


    }

    public void SetTapedBoxPrice()
    {
        _sell_price = _is_right ? 100 : 90;
    }
}
