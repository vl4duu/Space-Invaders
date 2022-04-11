using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Projectile
{   
    public float multiplier = 2f;
    public int timer = 3;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
            
            StartCoroutine(Pickup(other));
            
        }
    }

    private IEnumerator Pickup(Collider2D player){
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        // Speed up
        player.gameObject.GetComponent<Player>().speed *= multiplier;
         // Wait
        
        yield return new WaitForSeconds(this.timer);
        // Speed down
         player.gameObject.GetComponent<Player>().speed /= multiplier;
        Debug.Log("After");
        Destroy(gameObject);
        

        
    }

}
