using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersController : MonoBehaviour
{
    public GameObject initialSet;
    private GameObject currentSet;
    public float bulletSpeedMultiplier = 1.5f;
    public float movementSpeedMultiplier = 1.5f;


    void Start()
    {
        var position = this.transform.position;
        this.initialSet.transform.position.Set(position.x, position.y, position.z);
        currentSet = Instantiate(initialSet, position, Quaternion.identity);
        
    }
    
    void Update()
    {
        if (this.currentSet.GetComponent<Invaders>().amountAlive != 0) return;
        var position = this.transform.position;
        this.transform.position.Set(-position.x, position.y, position.z);
        position = this.transform.position;
        this.initialSet.transform.position.Set(position.x,position.y, position.z); 
        Destroy(currentSet);
        bulletSpeedMultiplier += 0.1f;
        movementSpeedMultiplier += 0.1f;
        currentSet = Instantiate(initialSet, this.transform.position, Quaternion.identity);
        currentSet.GetComponent<Invaders>().SpeedMultiplier += movementSpeedMultiplier;
        currentSet.GetComponent<Invaders>().BulletSpeedMultiplier *= bulletSpeedMultiplier;
    }
    
}
