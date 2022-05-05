using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTally : MonoBehaviour
{
    public GameObject Player;
    private int tally;

    private void Update()
    {
        tally = Player.GetComponent<Player>().lifes;
        RemoveHeart(tally);
    }

    private void RemoveHeart(int i)
    {
        if (i + 1 <= this.transform.childCount && i >= 0)
        {
            GameObject heart = this.gameObject.transform.GetChild(i).gameObject;
            if (heart.activeInHierarchy)
            {
                heart.SetActive(false);
            }
        }
    }
    
}
