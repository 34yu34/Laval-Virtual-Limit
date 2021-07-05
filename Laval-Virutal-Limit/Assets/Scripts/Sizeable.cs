using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sizeable : MonoBehaviour
{
    [SerializeField]
    private Size _size;

    public Size Size => _size;

}
