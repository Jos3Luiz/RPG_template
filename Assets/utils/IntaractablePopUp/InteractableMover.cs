using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMover : MonoBehaviour
{
    public float speed=2;
    public float amplitude = 0.05f;
    private float total_time;
    void Update()
    {
        total_time += Time.deltaTime;
        transform.position = new Vector3(transform.position.x ,transform.position.y + Mathf.Cos(total_time*speed)*amplitude, transform.position.z);
    }
}
