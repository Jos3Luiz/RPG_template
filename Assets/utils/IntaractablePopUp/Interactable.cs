using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Interactable : MonoBehaviour
{
    public delegate void FOnPopUp(bool isClose);
    public delegate void FOnInteract(GameObject player);

    public FOnPopUp onPrompt;
    public FOnInteract onInteract;
    

    public GameObject popUpPrefab;
    public Vector3 offset = Vector3.up;
    private GameObject reference;
    // Start is called before the first frame update

    
    //called by player
    public virtual void changePrompt(bool isActive){
        onPrompt?.Invoke(isActive);
        if (isActive && popUpPrefab!=null)
        {
            
            reference = Instantiate(popUpPrefab,  transform.position+offset,Quaternion.identity,transform);
        }
        else
        {
            Destroy(reference);
        }
    }

    //called by player
    public virtual void Interact(GameObject playerCaller)
    {
        onInteract?.Invoke(playerCaller);
        Debug.Log("interacted");
    }
    

    
}
