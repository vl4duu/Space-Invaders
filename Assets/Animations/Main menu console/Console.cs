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
    private System.Action delete;
    private System.Action newLine;
    private InputField input;
    private char character;
    private string tempString;
    public bool KeyDownEvent { get; set; }
    private bool tempBool;

    private void Awake()
    {
        input = GetComponent<InputField>();
        delete += DeleteChar;
        newLine += NewLine;
    }

    private void DeleteChar()
    {
        input.text = input.text.Remove(input.text.Length - 1, 1);
    }
    

    void FixedUpdate()
    {
        input.text += character;
        character = '\0';
        try
        {
            OnGUI();
        }
        catch (NullReferenceException)
        {
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (e.keyCode == KeyCode.LeftShift || e.keyCode == KeyCode.LeftShift)
            {
                e.keyCode = KeyCode.None;
            }
            else if (e.keyCode == KeyCode.Backspace)
            {
                try
                {
                    delete.Invoke();
                }
                catch (ArgumentOutOfRangeException)
                {
                }

                e.keyCode = KeyCode.None;
            }
            else if (e.keyCode == KeyCode.Return)
            {
                this.newLine.Invoke();
                tempBool = true;
                e.keyCode = KeyCode.None;
            }
            else
            {
                character = (char) e.keyCode;
            }
        }
    }

    void NewLine()
    {
        if(tempBool)
        {
            Debug.Log("Enter");
            tempString = input.text;
            GameObject parent = this.transform.parent.gameObject;
            float y = parent.transform.position.y - parent.transform.localScale.y;
            GameObject temp = Instantiate(parent, parent.transform.parent);
            tempBool = false;
        }
        
    }


}