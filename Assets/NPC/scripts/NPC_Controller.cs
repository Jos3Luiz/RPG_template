using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class NPC_Controller : MonoBehaviour
{
    //public bool isFriendly;
    //public float randomWalkTime =5;
    //public float sphereRadius = 10;
    // Start is called before the first frame update
    //private Vector3 startPoint;
    public float percepitonTickTime=0.5f;
    public float perceptionRadius=5f;
    private Vector3 targetPoint;
    private GameObject targetObject = null;
    
    public LayerMask l;

    private Utils u;
    private CharacterMovement charMov;
    void Start()
    {
        u = new Utils();
        charMov = GetComponent<CharacterMovement>();
        //startPoint = transform.position;
        StartCoroutine(PerceptionTick());
    }
/*
    private Vector3 getRandomVector(){
        float random = Random.Range(0f, 260f);
        Vector2 v = new Vector2(Mathf.Cos(random), Mathf.Sin(random)).normalized;
        return new Vector3 (v.x,0,v.y);
    }*/

    IEnumerator PerceptionTick()
    {
        //targetPoint = startPoint + getRandomVector()*sphereRadius;
        
        while(true)
        {
            //u.DebugSphereCast(transform.position,perceptionRadius , Vector3.zero,10f,l);
            /*RaycastHit[]hits = Physics.SphereCastAll(transform.position,perceptionRadius , Vector3.zero,10f);
            {
                foreach (RaycastHit r in hits)
                {
                    Debug.Log("hit:"+r.collider.tag);
                }
            }*/
            
            yield return new WaitForSeconds(percepitonTickTime);
        }
    }
    /*IEnumerator ChangePoint()
    {
        targetPoint = startPoint + getRandomVector()*sphereRadius;
        while(true)
        {
            if (canMove){
                targetPoint = startPoint + getRandomVector()*sphereRadius;
            }
            
            yield return new WaitForSeconds(randomWalkTime);
        }
    }*/

    private void MoveVector(Vector3 desiredPosition){
        Vector3 direction = desiredPosition-transform.position;
        if(direction.magnitude > 1){
            Vector2 input = new Vector2(direction.x,direction.z).normalized;
            charMov.MoveHorizontal(input.x);
            charMov.MoveVertical(input.y);
        }
        else{
            charMov.MoveHorizontal(0);
            charMov.MoveVertical(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        u.DebugSphereCast(transform.position,perceptionRadius , Vector3.zero,10f,l);
        if(targetObject){
            MoveVector(targetObject.transform.position);
        }
        else{
            MoveVector(targetPoint);
        }
    }

    public void AIMoveTo(Vector3 destination){
        targetPoint = destination;
        targetObject = null;
    }

    public void AIFollowTarget(GameObject target){
        targetObject = target;
    }
    
}
