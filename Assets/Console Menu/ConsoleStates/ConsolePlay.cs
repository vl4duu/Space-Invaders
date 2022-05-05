using System;
using UnityEngine;

namespace Animations.Console_Menu.ConsoleStates
{
    public class ConsolePlay : IConsoleState
    {
        

        public void Enter(IOConsole console)
        {
            
            console.StartCoroutine(console.PlaySetup());
        }

        public IConsoleState Tick(IOConsole console, string input)
        {
            console.OnKeyboardInput();
            if (console.exitOnNo)
            {
                console.exitOnNo = false;
                return new ConsoleMain();
            }
            if (input == "back") return new ConsoleMain();
            return null;
        }

        public void Exit(IOConsole console)
        {
            Debug.Log("Left ConsolePLay");
        }

        public override String ToString()
        {
            return "Play";
        }
        
    }
}