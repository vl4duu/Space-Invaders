using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update(){
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

        private void OnTriggerEnter2D(Collider2D other){
        // Debug.Log("Bang!");
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
            // Debug.Log('Activate');
            if(this.destroyed != null){
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
        }
    }
}
