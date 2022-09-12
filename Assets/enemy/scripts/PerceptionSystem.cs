using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PerceptionSystem : MonoBehaviour
{
    
    //public delegate void FOnPerceptionUpdated(Dictionary<GameObject, bool> perception_state);
    public delegate void FOnPerceptionUpdated(GameObject[] perception_state);
    public FOnPerceptionUpdated onPerceptionUpdate;
    
    public float percepitonTickTime=0.5f;
    public float perceptionRadius=2f;
    public LayerMask perceptionAwareLayer;

    

    //private Dictionary<GameObject, bool> _perception_state = new Dictionary<GameObject, bool>();
    //private Dictionary<GameObject, bool> _last_perception_state = new Dictionary<GameObject, bool>();
    
    
    
    //checks if current detected actors are the same of last tick and calls onPerceptionUpdate with the changed status
    /*IEnumerator PerceptionTick()
    {
        yield return null;
        while (true)
        {
            foreach (GameObject k in _perception_state.Keys.ToList())
            {
                _perception_state[k] = false;
            }
            
            GameObject[] overlaped = Physics.OverlapSphere(transform.position, perceptionRadius, perceptionAwareLayer)
                .Select(x => x.gameObject)
                .Where(x => x != gameObject)
                .ToArray();
            
            foreach (var g in overlaped)
            {
                _perception_state[g] = true;
            }

            if (_perception_state != _last_perception_state)
            {
                onPerceptionUpdate(_perception_state);    
            }

            //update perception_state
            _perception_state = _perception_state
                .Where(x => x.Value == true)
                .ToDictionary(p => p.Key, p =>p.Value);
            
            _last_perception_state = _perception_state;
            
            
            yield return new WaitForSeconds(percepitonTickTime);
        }
    }*/
    IEnumerator PerceptionTick()
    {
        yield return null;
        while (true)
        {
            GameObject[] overlaped = Physics.OverlapSphere(transform.position, perceptionRadius, perceptionAwareLayer)
                .Select(x => x.gameObject)
                .Where(x => x != gameObject)
                .ToArray();
            
            onPerceptionUpdate?.Invoke(overlaped);
            yield return new WaitForSeconds(percepitonTickTime);
        }
    }
    void OnEnable()
    {
        StartCoroutine(PerceptionTick());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,perceptionRadius);
    }
    
}
