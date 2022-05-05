using System;
using UnityEngine;

namespace Animations.Console_Menu.ConsoleStates
{
    public class ConsoleMain : IConsoleState
    {
        public void Enter(IOConsole console)
        {
            console.ClearConsole(false);
            console.MakeOutputLine("Swansea University [Version: 2019_2022]");
            console.MakeOutputLine("Vlad Mutilica <1910238>");
            console.MakeOutputLine("Type help for options!");
            console.MakeNewLine();
        }

        public IConsoleState Tick(IOConsole console, string input)
        {
            console.OnKeyboardInput();
            if (input == "play") return new ConsolePlay();
            return null;
        }

        public void Exit(IOConsole console)
        {
            Debug.Log("Left ConsoleMain");
        }
        
        public override String ToString()
        {
            return "Main";
        }
    }
}