using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimReciever : MonoBehaviour
{
    public bool isActing;
    public bool isFishing = false;
    public int fishingTaskCount = 0;
    public Animator animator;

    public void OnToolAnimationEnd()
    {
        isActing = false;
    }

    public void OnFishingAnimationEnd(int cnt)
    {

        isActing = false;

        fishingTaskCount = cnt;
        animator.SetInteger("FishingTaskCount", fishingTaskCount);

        if (fishingTaskCount == 4)
        {
            isFishing = false;
        }


        
        animator.SetBool("isFishing", isFishing);
    }
}
