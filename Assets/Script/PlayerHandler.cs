using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
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
        anim.wrapMode = WrapMode.Loop;
        anim.Play(animationClipName);

    }
}
