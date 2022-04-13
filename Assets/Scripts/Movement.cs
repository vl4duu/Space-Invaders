using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    
    public void MoveRight()
    {
        // Mathf.SmoothDamp(0, 1, , 2);
        this.transform.position -= Vector3.left * this.speed * Time.deltaTime;
    }

    public void MoveLeft()
    {
        this.transform.position += Vector3.left * this.speed * Time.deltaTime;
    }
}