using System;

namespace Animations.Console_Menu.ConsoleStates
{
    public interface IConsoleState
    {
        public void Enter(IOConsole console);
        public IConsoleState Tick(IOConsole console, String input);
        public void Exit(IOConsole console);

        public String ToString();
    }
}