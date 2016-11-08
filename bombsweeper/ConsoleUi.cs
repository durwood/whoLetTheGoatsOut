using System;

namespace bombsweeper
{
    internal class ConsoleUi : IUi
    {
        internal ConsoleUi()
        {
            Console.CursorVisible = false;
            Console.Clear();
        }
    }
}