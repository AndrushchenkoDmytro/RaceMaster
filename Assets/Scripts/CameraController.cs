using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform car;
    [SerializeField] private float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.Lerp(transform.position, car.position, Time.deltaTime * speed);
        movement.y = transform.position.y;
        transform.position = movement;
    }
}
