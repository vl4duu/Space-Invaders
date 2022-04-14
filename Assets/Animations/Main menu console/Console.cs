using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Console : MonoBehaviour
{
    private InputField input;
    private char character;
    private bool backspace;

    private void Awake()
    {
        input = GetComponent<InputField>();
    }



    private void Update()
    {
        try
        {
            OnGUI();
        }
        catch (NullReferenceException)
        {
        }
    }

    void FixedUpdate()
    {
        input.text += character;
        character = '\0';
        if (backspace)
        {
            string temp = input.text.Remove(input.text.Length - 1, 1);
            input.text = temp;
            backspace = false;
            Debug.Log(input.text.Remove(input.text.Length - 1, 1));
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (e.keyCode == KeyCode.Backspace)
            {
                backspace = true;
            }
            character = (char) e.keyCode;
        }
    }

    public bool KeyDownEvent { get; set; }
}