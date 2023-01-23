using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerHandler : MonoBehaviour
{
    public Animation anim;
 
    void Start()
    {
        this.gameObject.SetActive(false);
        
        anim = GetComponent<Animation>();
        foreach (AnimationState state in anim)
        { 
            Debug.Log(state.name);
        }
    }

    public void SetAnimation(string animationClipName)
    {
        anim.wrapMode = WrapMode.Loop;
        anim.Play(animationClipName);
    }
}
