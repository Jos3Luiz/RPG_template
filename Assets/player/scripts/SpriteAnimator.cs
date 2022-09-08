using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct AnimationConfig
{
    public Sprite[] SpriteArray;
    public float fps;
    public bool loop;
    public bool dontInterrupt;
    public int nextAnim;
    public Sprite GetSprite(int index)
    {
        return SpriteArray[index % SpriteArray.Length];
    }
}




[RequireComponent(typeof(SpriteRenderer))]

public class SpriteAnimator : MonoBehaviour
{
    public AnimationConfig[] animationList;
    
    private SpriteRenderer sr;
    private float count;

    int animationIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

    }
    

    public void RenderState()
    {
        AnimationConfig currentAnimConfig = animationList[animationIndex];
        sr.sprite = currentAnimConfig.GetSprite((int)(count * currentAnimConfig.fps));
        count+=Time.deltaTime;
        if (count > currentAnimConfig.fps && !currentAnimConfig.loop){
            animationIndex =  currentAnimConfig.nextAnim;
            count =0;
        }
    }

    public void SetState(int state,bool reset = false){
        if(state==animationIndex && !reset)
            return;
        if (!animationList[animationIndex].dontInterrupt){
            animationIndex = state;
            count=0;
        }
    }

    public void Update()
    {
        
        RenderState();
    }
}
