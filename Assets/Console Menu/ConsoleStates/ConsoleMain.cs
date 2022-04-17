using System;
using UnityEngine;

namespace Animations.Console_Menu.ConsoleStates
{
    public class ConsoleMain : IConsoleState
    {
        public void Enter(IOConsole console)
        {
            Debug.Log("Entered ConsoleMenu");
            console.MakeNewLine();
        }

        public IConsoleState Tick(IOConsole console, string input)
        {
            console.OnKeyboardInput();
            if (input == "help")
            {
                console.MakeMenu(console.HelpMenu);
                console.tempValue = null;
            }
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