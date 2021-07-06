using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCleaner : MonoBehaviour
{
    void Start()
    {
        foreach(var throwable in FindObjectsOfType<Valve.VR.InteractionSystem.Throwable>())
        {
            Destroy(throwable.gameObject);
        }
    }
}
