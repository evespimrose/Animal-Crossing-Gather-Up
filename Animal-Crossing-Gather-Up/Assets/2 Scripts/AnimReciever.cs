using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimReciever : MonoBehaviour
{
    public bool isActing;

    public void OnToolAnimationEnd()
    {
        isActing = false;
    }
}
