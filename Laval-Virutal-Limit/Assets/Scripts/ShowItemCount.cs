using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItemCount : MonoBehaviour
{
    [SerializeField]
    private TextMesh _text;

    [SerializeField]
    private Belt _belt;

    private int _current_count = 0;
    private int current_count
    {
        get => _current_count;
        set
        {
            if (_current_count == value) return;
            _current_count = value;
            on_change();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        current_count = _belt.ItemsLeft;
    }

    // Update is called once per frame
    void Update()
    {
        current_count = _belt.ItemsLeft;

    }

    private void on_change()
    {
        _text.text = $"Remaining items:\n{current_count}";
    }
}
