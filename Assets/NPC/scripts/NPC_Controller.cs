using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



[RequireComponent(typeof(CharacterMovement))]
public class NPC_Controller : MonoBehaviour
{

    public delegate void FOnPerceptionUpdated(GameObject[] arr);

    public FOnPerceptionUpdated onPerceptionUpdate;
    
    
    public float percepitonTickTime=0.5f;
    public float perceptionRadius=2f;
    public LayerMask perceptionAwareLayer;
    
    private Vector3 targetPoint;
    private GameObject targetObject;
    private bool isTracking=false;
    private bool finishedTracking=false;
    
    private CharacterMovement charMov;
    void Start()
    {
        charMov = GetComponent<CharacterMovement>();
        
        StartCoroutine(PerceptionTick());
    }
    
    IEnumerator PerceptionTick()
    {
        yield return null;
        while(true)
        {
            GameObject[] results = Physics.OverlapSphere(transform.position, perceptionRadius, perceptionAwareLayer)
                .Select(x => x.gameObject)
                .Where(x => x != gameObject)
                .ToArray();
            if (results.Length > 0)
            {
                onPerceptionUpdate(results);
            }
            
            yield return new WaitForSeconds(percepitonTickTime);
        }
    }
    
    
    
    private void MoveToVector(Vector3 desiredPosition){
        Vector3 direction = (desiredPosition-transform.position);
        direction.y = 0;
        Vector2 input = new Vector2(direction.x,direction.z).normalized;
        if (direction.magnitude > 0.1f)
        {
            charMov.MoveHorizontal(input.x);
            charMov.MoveVertical(input.y);
        }
        else
        {
            finishedTracking = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject)
        {
            targetPoint = targetObject.transform.position;
        }

        if (!finishedTracking || isTracking)
        {
            MoveToVector(targetPoint);
        }
    }

    public void AIMoveTo(Vector3 destination,bool track=false){
        targetPoint = destination;
        targetObject = null;
        isTracking = track;
        finishedTracking = false;
    }
    
    public void AIFollowTarget(GameObject target,bool track=false){
        targetObject = target;
        isTracking = track;
        finishedTracking = false;
    }

    public void Jump()
    {
        charMov.Jump(0);
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,perceptionRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,targetPoint );
        
    }
    
}
