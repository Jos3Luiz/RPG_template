using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAnimSelector
{
Idle,
walk
};

[System.Serializable]
public struct AnimationConfig
{
    public Sprite[] SpriteArray;
    public float fps;
    
    public Sprite GetSprite(int index)
    {
        return SpriteArray[index % SpriteArray.Length];
    }
}




[RequireComponent(typeof(SpriteRenderer))]

public class SpriteAnimator : MonoBehaviour
{
    public AnimationConfig Idle;
    public AnimationConfig Walk;
    
    private SpriteRenderer sr;
    private float count ;
    public EAnimSelector animState;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

    }
    

    public void RenderState()
    {
        
        switch (animState)
        {
            case EAnimSelector.Idle:
                sr.sprite = Idle.GetSprite((int)(count * Idle.fps));
                break;
            case EAnimSelector.walk:
                sr.sprite =  Walk.GetSprite((int)(count*Walk.fps));
                break;
        }   
        
        count+=Time.deltaTime;
    }

    public void Update()
    {
        
        RenderState();
    }
}
