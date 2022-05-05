using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTally : MonoBehaviour
{
    public GameObject player;
    private int tally;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tally = player.GetComponent<Player>().score;
        this.transform.GetComponent<Text>().text = tally.ToString();
    }
}