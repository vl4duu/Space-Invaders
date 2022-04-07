using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    private void Update(){
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other){
        // Debug.Log("Bang!");
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
            if(this.destroyed != null){
                this.destroyed.Invoke();
            }
        }
        Destroy(this.gameObject);
    }
}
