using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animations.Console_Menu;
using Animations.Console_Menu.ConsoleStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IOConsole : MonoBehaviour
{
    public InputField input;
    public GameObject HelpMenu;
    public GameObject newline;
    public GameObject volume;
    public GameObject output;
    public float lower;
    private GameObject activeLine;
    private float overflowHeight;
    private GameObject container;
    public IConsoleState currentState = new ConsoleMain();
    public String tempValue;
    public PlayerScores scores = new PlayerScores();
    private float overflowTally;
    public bool exitOnNo;

    

    private void Awake()
    {
        MakeNewLine();
        container = this.transform.GetChild(1).GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        CheckForOverflow();
    }

    private void Update()
    {
        CheckForOverflow();
        UpdateState();
    }

    private void UpdateState()
    {
        IConsoleState newState = null;

        try
        {
            newState = currentState.Tick(this, tempValue);
            if (newState != null)
            {
                currentState.Exit(this);
                currentState = newState;
                newState.Enter(this);
            }
        }
        catch (NullReferenceException)
        {
        }
    }

    public void OnKeyboardInput()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (input.text.Length != 0)
                {
                    input.text = input.text.Substring(0, input.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                tempValue = input.text.ToLower();
                if (tempValue == "clear")
                {
                    ClearConsole();
                }
                else
                {
                    switch (currentState.ToString())
                    {
                        case "Main":

                            switch (tempValue)
                            {
                                case "play":
                                    break;
                                case "leaderboard":
                                    MakeLeaderBoard(scores.getScoreList());
                                    MakeNewLine();
                                    break;
                                default:
                                    MakeNewLine();
                                    break;
                            }

                            break;
                        case "Play":
                            break;
                    }
                }
            }
            else
            {
                input.text += c;
            }
        }
    }

    public void MakeNewLine()
    {
        activeLine = Instantiate(newline, this.transform.GetChild(1).Find("Container").transform);
        activeLine.transform.position += Vector3.down * lower;
        lower += activeLine.GetComponent<RectTransform>().rect.height;
        String location = activeLine.GetComponent<Text>().text.Replace("*", currentState.ToString());
        activeLine.GetComponent<Text>().text = location;
        input = activeLine.transform.GetChild(0).gameObject.GetComponent<InputField>();
        input.enabled = false;
    }

    public void MakeOutputLine(String o, bool makeNewLine = false)
    {
        int lineNo = CountLineNumber(o);
        GameObject menu = Instantiate(output, this.transform.GetChild(1).Find("Container").transform);
        menu.transform.position += Vector3.down * lower;
        menu.GetComponent<BoxCollider2D>().size = new Vector2(100, lineNo * 100);
        menu.GetComponent<Text>().text = o;
        lower += lineNo * 50;

        if (makeNewLine) MakeNewLine();
    }

    public void MakeLeaderBoard(List<String> list)
    {
        MakeOutputLine("LeaderBoard");
        for (int i = 0; i < list.Count; i++)
        {
            MakeOutputLine(list[i]);
        }
    }

    public void MakeMenu(GameObject obj)
    {
        GameObject menu = Instantiate(obj, this.transform.GetChild(1).Find("Container").transform);
        lower += activeLine.GetComponent<RectTransform>().rect.height;
        menu.transform.position += Vector3.down * lower;
        lower += menu.GetComponent<RectTransform>().rect.height;
        MakeNewLine();
    }

    public int CountLineNumber(String text)
    {
        int lines = text.Count(f => (f == '\n'));
        return lines + 1;
    }

    public void CheckForOverflow()
    {
        if (container.transform.childCount > 0)
        {
            foreach (Transform child in container.transform)
            {
                overflowHeight = child.GetComponent<RectTransform>().rect.height;
                child.GetComponent<OverflowCheck>().overflow += MoveUp;
            }
        }
    }

    public void MoveUp()
    {
        Destroy(container.transform.GetChild(0).gameObject);
        if (container.transform.childCount > 20)
        {
            for (int i = 0; i < 5; i++)
            {
                Destroy(container.transform.GetChild(i).gameObject);
            }
        }

        overflowTally += overflowHeight;
        container.transform.position += Vector3.up * overflowHeight;
    }

    public void ResetHeight()
    {
        container.transform.position += Vector3.down * overflowTally;
    }

    public void ClearConsole()
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }

        ResetHeight();
        lower = 0;
        overflowTally = 0;
        MakeNewLine();
    }


    public IEnumerator PlaySetup()
    {
        scores.PopulateDict();
        MakeLeaderBoard(scores.getScoreList());
        MakeOutputLine("What is your name?", true);
        // CheckForOverflow();
        Debug.Log(!Input.GetKeyDown(KeyCode.Return));
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }

        if (tempValue == "back") yield break;
        if (scores.PlayerExists(tempValue))
        {
            MakeOutputLine("Welcome back " + tempValue + " !");
            StartCoroutine(PlayQuery());
        }
        else
        {
            scores.AddPlayer(tempValue);
            scores.SaveScores();
            MakeOutputLine("Player added !");
            StartCoroutine(PlayQuery());
        }

    }

    public IEnumerator PlayQuery()
    {
        MakeOutputLine("Do you want to play? (Y/N)", true);
            
        tempValue = null;
        while (tempValue == null)
        {
            yield return null;
        }

        switch (tempValue)
        {
            case "y":
                SceneManager.LoadScene("Game");
                break;
            case "n":
                exitOnNo = true;
                break;

        }
    }

}