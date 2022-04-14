using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {
            Animator animator = this.GetComponent<Animator>();
            animator.SetTrigger("click");
        }
    }
}
