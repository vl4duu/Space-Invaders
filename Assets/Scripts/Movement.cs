using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 leftEdge;
    private  Vector3 rightEdge;

    private void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            && this.transform.position.x >= (leftEdge.x + 1f) )
        {
            MoveLeft();
        }
        
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && this.transform.position.x <= (rightEdge.x - 1f))
        {
            MoveRight();
        }
    }
    
    public void MoveRight()
    {
        this.transform.position -= Vector3.left * speed * Time.deltaTime;
    }

    public void MoveLeft()
    {
        this.transform.position += Vector3.left * speed * Time.deltaTime;
    }
}