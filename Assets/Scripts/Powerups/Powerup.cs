using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class Powerup : Projectile
{
    public bool isActive;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(Pickup(other));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private IEnumerator Pickup(Collider2D player)
    {
        int timer = this.GetComponent<Buff>().timer;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        // Apply
        isActive = this.GetComponent<Buff>().ApplyBuff(player);
        // Wait
        yield return new WaitForSeconds(timer);
        // Debuff
        isActive = this.GetComponent<Buff>().Debuff(player);

        Destroy(gameObject);
    }





}