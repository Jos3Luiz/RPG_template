using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(NPC_Controller))]
public class NPC_Behaviour : Base_Behaviour
{
    public float randomWalkRadius=2f;
    public float randomWalkTime=5f;

    private Interactable _interactable;
    private CharacterMovement _characterMovement;
    public override void Start()
    {
        base.Start();
        _characterMovement = GetComponent<CharacterMovement>();
        _interactable = GetComponent<Interactable>();
        if (_interactable)
        {
            _interactable.onPrompt += OnPrompt;
        }

    }
    protected override float BehaviourTick()
    {
        npcController.AIRandomWalk(randomWalkRadius);
        return randomWalkTime;
    }

    protected void OnPrompt(bool s)
    {
        _characterMovement.SetCanMove(!s);
    }
    
}
