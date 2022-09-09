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
    private float distToGround;

    private Rigidbody rb;
    private SpriteAnimator spriteAnim;
    private Collider _collider;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteAnim = GetComponent<SpriteAnimator>();
        _collider = GetComponent<Collider>();
        distToGround = _collider.bounds.extents.y;
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
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        /*RaycastHit rayHit;
        float characterHeight = GetComponent<BoxCollider> ().size.y;
        float characterWidth = GetComponent<BoxCollider> ().size.x;
        Vector3 startPoint = transform.position;
        return Physics.SphereCast (startPoint, distToGround+1f, Vector3.down, out rayHit, distToGround+1f);  */

    }
    public void Jump(float input)
    {
        if (IsGrounded())
        {
            rb.AddForce(0, jumpForce, 0);
            spriteAnim.SetState(2);
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
            spriteAnim.SetState(1);
            
        }
        else
        {
            spriteAnim.SetState(0);
        }
        
    }

    public void FixedUpdate()
    {
        Vector3 speed = new Vector3(CurrentSpeed.x, 0, CurrentSpeed.y);
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime);
    }
}