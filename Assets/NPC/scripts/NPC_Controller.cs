using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterMovement))]
public class NPC_Controller : MonoBehaviour
{

    
    
    private Vector3 targetPoint;
    private GameObject targetObject;
    private bool isTracking=false;
    private bool finishedTracking=false;
    private Vector3 startPoint;
    
    private CharacterMovement charMov;
    void Start()
    {
        charMov = GetComponent<CharacterMovement>();
        targetPoint = transform.position;
        startPoint = transform.position;
    }


    private void MoveToVector(Vector3 desiredPosition){
        Vector3 direction = (desiredPosition-transform.position);
        direction.y = 0;
        Vector2 input = new Vector2(direction.x,direction.z).normalized;
        
        //if too too close
        if (direction.magnitude < 0.1f)
        {
            finishedTracking = true;
        }
        else
        {
            charMov.MoveHorizontal(input.x);
            charMov.MoveVertical(input.y);
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

    public void AIStop()
    {
        finishedTracking = true;
    }
    
    private static Vector3 GetRandomVector(){
        float random = Random.Range(0f, 260f);
        Vector2 v = new Vector2(Mathf.Cos(random), Mathf.Sin(random)).normalized;
        return new Vector3 (v.x,0,v.y);
    }
    
    public void AIRandomWalk(float sphereRadius,bool resetPosition=false)
    {
        if (resetPosition)
        {
            startPoint = transform.position;
        }
        AIMoveTo(startPoint + GetRandomVector() * sphereRadius);
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,targetPoint );
        
    }
    
}
