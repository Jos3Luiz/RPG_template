using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteAnimator))]
public class CharacterMovement : MonoBehaviour
{
    public float accelerationMultiplier = 10f;
    public float MaxSpeed = 8.0f;
    public float DragCoefficient = 4f;
    public float jumpForce = 20000;
    
    private Vector2 CurrentSpeed;
    private Vector2 AccelerationDirection;
    private float rotSpeed;
    
    private Rigidbody rb;
    private SpriteAnimator spriteAnim;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteAnim = GetComponent<SpriteAnimator>();

    }

    public void MoveHorizontal(float input)
    {
        AccelerationDirection.x = input;
        if (input != 0)
        {
            rotSpeed = AccelerationDirection.x;
        }
    }

    public void MoveVertical(float input)
    {
        AccelerationDirection.y = input;
    }

    public void Jump(float input)
    {
        if (Mathf.Abs(rb.velocity.y) < 0.5)
        {
            rb.AddForce(0, jumpForce, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurrentSpeed = Vector2.ClampMagnitude(CurrentSpeed - (CurrentSpeed * (DragCoefficient * Time.fixedDeltaTime)) +
                                              AccelerationDirection.normalized *
                                              (accelerationMultiplier * Time.fixedDeltaTime), MaxSpeed);
                                              
        if (rotSpeed != 0)
        {
            Quaternion toRot = Quaternion.LookRotation(new Vector3(0, 0, rotSpeed), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, 720 * Time.deltaTime);
        }

        if (CurrentSpeed.magnitude > 0.1)
        {
            spriteAnim.animState = EAnimSelector.walk;
            
        }
        else
        {
            spriteAnim.animState = EAnimSelector.Idle;
        }
    }

    public void FixedUpdate()
    {
        Vector3 speed = new Vector3(CurrentSpeed.x, 0, CurrentSpeed.y);
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime);
    }
}