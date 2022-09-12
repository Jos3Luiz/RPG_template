using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteAnimator))]
[RequireComponent(typeof(Collider))]
public class CharacterMovement : MonoBehaviour
{
    public float accelerationMultiplier = 10f;
    public float MaxSpeed = 8.0f;
    public float DragCoefficient = 4f;
    public float jumpForce = 20000;
    
    
    private Vector2 CurrentSpeed;
    private Vector2 AccelerationDirection;
    private float rotSpeed;
    private bool _canMove=true;
    
    private Rigidbody rb;
    private SpriteAnimator spriteAnim;
    private Collider _collider;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spriteAnim = GetComponent<SpriteAnimator>();
        _collider = GetComponent<Collider>();
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

    public bool IsGrounded(){
        //removes collider of own character
        Vector2 bounds = _collider.bounds.extents;
        return (Physics.OverlapSphere(transform.position + Vector3.down*bounds.y,bounds.x/2).Length > 1);
    }

    public void Jump(float input)
    {
        if (IsGrounded() && _canMove)
        {
            rb.AddForce(0, jumpForce, 0);
            spriteAnim.SetState(2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canMove)
        {
            spriteAnim.SetState(0);
            return;
        }
            
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
            spriteAnim.SetState(1);
        }
        else
        {
            spriteAnim.SetState(0);
        }

        AccelerationDirection = Vector2.zero;

    }

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
    }

    public void FixedUpdate()
    {
        if (_canMove)
        {
            Vector3 speed = new Vector3(CurrentSpeed.x, 0, CurrentSpeed.y);
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime);            
        }
    }
}