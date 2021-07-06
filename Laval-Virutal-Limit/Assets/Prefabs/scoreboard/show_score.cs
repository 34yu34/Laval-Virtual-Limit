using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_score : MonoBehaviour
{
    private TextMesh _text_mesh;
    // Start is called before the first frame update
    void Start()
    {
        _text_mesh = GetComponent<TextMesh>();
        _text_mesh.text = $"{MoneyManager.Instance.Note}%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
