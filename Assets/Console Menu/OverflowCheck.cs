using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowCheck : MonoBehaviour
{
    public System.Action overflow;



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Too low");
        overflow.Invoke();
    }
}
