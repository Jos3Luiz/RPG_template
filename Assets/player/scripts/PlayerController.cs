using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
 

    private CharacterMovement charMovement;
    private InputMaster controls;
    void Awake()
    {
        charMovement = GetComponent<CharacterMovement>();
        controls = new InputMaster();
        controls.Player.MovementAD.performed += ctx  => MovementAD(ctx);
        controls.Player.MovementWS.performed += ctx  => MovementWS(ctx);
        controls.Player.Jump.performed += ctx  => Jump(ctx);
    }

    private void MovementAD(InputAction.CallbackContext ctx)
    {
        charMovement.MoveHorizontal(ctx.ReadValue<float>());
    }
    
    private void MovementWS(InputAction.CallbackContext ctx)
    {
        charMovement.MoveVertical(ctx.ReadValue<float>());

    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        charMovement.Jump(0);
    }

    public void OnEnable()
    {
        
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }

    
}
