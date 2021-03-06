using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ConveyorPad : MonoBehaviour
{
    public float Speed { get; set; }

    public WrappableLink Link { get; set; }

    private Vector3 direction;

    readonly float DISTANCE_CHECK = 0.01f;

    private void Start()
    {
        setup_collider();
    }

    private void setup_collider()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void FixedUpdate()
    {
        advance();
    }

    private void advance()
    {
        Vector3 movement = Speed * direction * Time.fixedDeltaTime;
        transform.position += movement;
    }

    public void SetNextDestination(Vector3 destination)
    {
        direction = get_normalized_direction(destination);
    }

    private Vector3 get_normalized_direction(Vector3 destination)
    {
        return (destination - transform.position).normalized;
    }
}
