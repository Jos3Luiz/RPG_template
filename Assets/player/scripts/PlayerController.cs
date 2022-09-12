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
    private Vector2 input;
    private Interactable interactableTarget;
    void Awake()
    {
        charMovement = GetComponent<CharacterMovement>();
        controls = new InputMaster();
        controls.Player.MovementAD.performed += ctx  => MovementAD(ctx);
        controls.Player.MovementWS.performed += ctx  => MovementWS(ctx);
        controls.Player.Jump.performed += ctx  => Jump(ctx);
        controls.Player.Interact.performed += ctx  => Interact(ctx);
    }

    protected void MovementAD(InputAction.CallbackContext ctx)
    {
        input.x = ctx.ReadValue<float>();
    }
    
    protected void MovementWS(InputAction.CallbackContext ctx)
    {
        input.y = ctx.ReadValue<float>();
    }

    protected void Jump(InputAction.CallbackContext ctx)
    {
        charMovement.Jump(0);
    }

    protected void Interact(InputAction.CallbackContext ctx)
    {
        if (interactableTarget)
        {
            interactableTarget.Interact(gameObject);
        }
    }

    public void Update()
    {
        charMovement.MoveHorizontal(input.x);
        charMovement.MoveVertical(input.y);
    }

    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Interactable comp = collision.gameObject.GetComponent<Interactable>();
        if (!comp)
            return;
        
        //interactableTarget is null, then interactableTarget=new comp
        if (!interactableTarget)
        {
            interactableTarget = comp;
            comp.changePrompt(true);
        }
        //check if new interactable is closer
        else
        {
            if ((transform.position - comp.transform.position).magnitude
                < (transform.position - interactableTarget.transform.position).magnitude)
            {
                interactableTarget.changePrompt(false);
                interactableTarget = comp;
                comp.changePrompt(true);
            }
        }
        
        
    }

    public void OnCollisionExit(Collision collision)
    {
        Interactable comp = collision.gameObject.GetComponent<Interactable>();
        if (!comp)
            return;
        if (comp == interactableTarget)
        {
            comp.changePrompt(false);
            interactableTarget = null;
        }
    }
}
