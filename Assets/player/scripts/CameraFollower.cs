using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed=0.5f;
    public float distance=5f;
    
    public Vector3 offset = Vector3.back;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset*distance, Time.deltaTime *cameraSpeed);
    }
}
