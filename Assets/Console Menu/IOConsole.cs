using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animations.Console_Menu;
using Animations.Console_Menu.ConsoleStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
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
    private float overflowTally;
    [HideInInspector] public bool exitOnNo;
    public GameObject sceneTransition;
    public GameObject scoreManager;
    private PlayerScores leaderboard;
    

    private void Awake()
    {
        leaderboard = scoreManager.GetComponent<PlayerScores>();
        MakeOutputLine("Swansea University \n[Version: 2019_2022]");
        MakeOutputLine("Vlad Mutilica <1910238>");
        MakeOutputLine("Type help for options!");
        MakeNewLine();
        container = this.transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        CheckForOverflow();
    }

    private void Update()
    {
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
                                    
                                    MakeLeaderBoard(leaderboard.GetScoreList());
                                    MakeNewLine();
                                    break;
                                case "credits":
                                    ShowCredits();
                                    break;
                                case "help":
                                    ShowHelp();
                                    break;
                                case "quit":
                                    Application.Quit();
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
        activeLine = Instantiate(newline, this.transform.Find("Container"));
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
        GameObject menu = Instantiate(output, this.transform.Find("Container").transform);
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
            MakeOutputLine(">>" + list[i]);
        }
    }

    public void ShowHelp()
    {
        MakeOutputLine(">>  Play -> Boot the game.");
        MakeOutputLine(">> Back -> Go back to main menu");
        MakeOutputLine(">>  Leaderboard -> Shows leaderboard.");
        MakeOutputLine(">>  Clear  -> Clear console.");
        MakeOutputLine(">>  Credits -> Show credits.");
        MakeOutputLine(">> Quit -> Exit the game");
        MakeNewLine();
    }

    private void ShowCredits()
    {
        MakeOutputLine(">>  Menu background by Kenze Wee");
        MakeOutputLine(">>  Game background by VulpsVulps");
        MakeOutputLine(">>  Menu music by Moot");
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
        if (container.transform.childCount > 25)
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

    public void ClearConsole(bool newLine = true)
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }

        ResetHeight();
        lower = 0;
        overflowTally = 0;
        if (newLine) MakeNewLine();
    }


    public IEnumerator PlaySetup()
    {
        MakeLeaderBoard(leaderboard.GetScoreList());
        MakeOutputLine("What is your name?", true);
        CheckForOverflow();
        Debug.Log(!Input.GetKeyDown(KeyCode.Return));
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        
        if (tempValue == "back") yield break;
        if (leaderboard.PlayerExists(tempValue))
        {
            leaderboard.SelectPlayer(tempValue);
            MakeOutputLine("Welcome back " + tempValue + " !");
            StartCoroutine(PlayQuery());
        }
        else
        {
            leaderboard.AddPlayer(tempValue);
            MakeOutputLine("Player added !");
            StartCoroutine(PlayQuery());
        }
        yield return null;
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
                Instantiate(sceneTransition);
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("Game");
                break;
            case "n":
                exitOnNo = true;
                break;
        }
    }
}