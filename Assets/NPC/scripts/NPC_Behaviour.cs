using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPC_Controller))]
public class NPC_Behaviour : MonoBehaviour
{
    
    
    private Vector3 getRandomVector(){
        float random = Random.Range(0f, 260f);
        Vector2 v = new Vector2(Mathf.Cos(random), Mathf.Sin(random)).normalized;
        return new Vector3 (v.x,0,v.y);
    }
    
    public float randomWalkTime =5;
    public float sphereRadius = 2;
    private Vector3 startPoint;

    private NPC_Controller _npcController;
    
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        _npcController = GetComponent<NPC_Controller>();
        _npcController.onPerceptionUpdate += OnPerceptionChange;
        StartCoroutine(ApplyBehaviour());
        
    }

    //can be used for implementing a state machine such as patroll patterns
    protected virtual void ChoseNPCAction()
    {
        Vector3 targetPoint = startPoint + getRandomVector() * sphereRadius;
        _npcController.AIMoveTo(targetPoint);
    }
    
    IEnumerator ApplyBehaviour()
    {
        yield return null;
        while (true)
        {
            ChoseNPCAction();
            yield return new WaitForSeconds(randomWalkTime);
        }
    }

    protected virtual void OnPerceptionChange(GameObject[] arr)
    {
        _npcController.Jump();
    }
}
