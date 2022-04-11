using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InvadersController : MonoBehaviour
{
    public GameObject invaders;
    private GameObject currentSet;
    void Start()
    {
        var position = this.transform.position;
        this.invaders.transform.position.Set(position.x, position.y, position.z);
        currentSet = Instantiate(invaders, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.currentSet.GetComponent<Invaders>().amountAlive != 0) return;
        Destroy(currentSet);
        currentSet = Instantiate(invaders, this.transform.position, Quaternion.identity);
        currentSet.GetComponent<Invaders>().speedMultiplier += 0.5f;
        currentSet.GetComponent<Invaders>().activeMissile.speed += 100;
        // currentSet.transform.position += Vector3.up * Time.deltaTime;
    }
    
}
