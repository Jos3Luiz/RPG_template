using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(PerceptionSystem))]
public class EnemyBehaviour : Base_Behaviour
{
    private PerceptionSystem _perceptionSystem;
    private CharacterMovement _characterMovement;
    private bool isChasing;
    private GameObject target;
    private new void Start()
    {
        base.Start();
        _characterMovement = GetComponent<CharacterMovement>();
        _perceptionSystem = GetComponent<PerceptionSystem>();
        _perceptionSystem.onPerceptionUpdate += OnPerceptionChange;
    }
    
    protected virtual void OnPerceptionChange(GameObject[] perception_state)
    {
        if ( perception_state.Length>0 )
        {
            OnDetectPlayer(perception_state[0]);    
        }
        else
        {
            OnLostPlayer();
        }
    }

    protected virtual void OnDetectPlayer(GameObject player)
    {
        if (target!=player)
        {
            target=player;
            _characterMovement.Jump(0);
            BehaviourTick();
        }
        
    }

    protected virtual void OnLostPlayer()
    {
        if (target)
        {
            target=null;
            BehaviourTick();
        }
    }

    protected override float BehaviourTick()
    {
        base.BehaviourTick();
        if (target)
        {
            npcController.AIFollowTarget(target,true);
        }
        else
        {
            npcController.AIRandomWalk(2);
        }

        return 5;
    }
}
