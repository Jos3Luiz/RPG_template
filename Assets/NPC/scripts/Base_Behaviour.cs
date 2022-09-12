using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(NPC_Controller))]
public class Base_Behaviour : MonoBehaviour
{
    
    
    public bool randomizeStart = true;
    protected NPC_Controller npcController;
    
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        npcController = GetComponent<NPC_Controller>();
    }
    
    //allow for ApplyBehaviour to be customized, returning at dynamic times
    protected IEnumerator ApplyBehaviour()
    {
        float randomStart = 1f;
        float timeUntilNextFrame;
        if (randomizeStart)
        {
            randomStart = Random.Range(0, 2);
        }
        yield return new WaitForSeconds(randomStart);    
        
        
        while (true)
        {
            timeUntilNextFrame = BehaviourTick();
            yield return new WaitForSeconds(timeUntilNextFrame);
        }
    }

    //can be used for implementing a state machine such as patroll patterns
    protected virtual float BehaviourTick()
    {
        return 5;
    }

    private void OnEnable()
    {
        StartCoroutine(ApplyBehaviour());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}