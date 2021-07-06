using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class simple_button : MonoBehaviour
{
    [SerializeField]
    private string next_scene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.GetComponentInParent<HandCollider>())
        {
            SceneManager.LoadScene(next_scene, LoadSceneMode.Single);
        }
    }

}
