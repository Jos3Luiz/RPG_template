using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private float currentHitDistance;
    Vector3 _start;
    float _sphereRadius;
    Vector3 _direction;
    float _maxDistance;
    LayerMask _layerMask;

    public RaycastHit DebugSphereCast(Vector3 start,float sphereRadius,Vector3 direction,float maxDistance,LayerMask layerMask){
        _start=start;
        _sphereRadius=sphereRadius;

        _direction=direction;
        Debug.Log("casting");
        RaycastHit hit;
        if(Physics.SphereCast(start,sphereRadius , direction,out hit,maxDistance,layerMask)){
            currentHitDistance=hit.distance;
        }
        else{
            currentHitDistance = maxDistance;
        }
        return hit;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Debug.DrawLine(_start,_start+_direction*currentHitDistance);
        Gizmos.DrawWireSphere(_start+_direction*currentHitDistance,_sphereRadius);
    }
}
