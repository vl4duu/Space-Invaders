using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    private GameObject input;

    private void Awake()
    {
        input = GameObject.Find("Input");
    }

    private void Start()
    {
        if (input.GetComponent<InputField>().text == "Hello")
        {
            Debug.Log("Hello! :D");
        }
    }
}
