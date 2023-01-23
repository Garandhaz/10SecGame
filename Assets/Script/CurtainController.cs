using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainController : MonoBehaviour
{
    public Animation anim;
 
    void Start()
    {
        anim = GetComponent<Animation>();
        foreach (AnimationState state in anim)
        { 
            Debug.Log(state.name);
        }
    }

    public void SetAnimation(string animationClipName)
    {
        anim.Play(animationClipName);
    }
}
