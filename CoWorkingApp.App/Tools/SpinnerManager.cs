using System;

namespace CoWorkingApp.App.Tools
{
    public static class SpinnerManager
    {
        public static void Show()
        {
            for(var i=0; i<15; i++)
            {
                switch(i%4)
                {
                    case 0:
                    Console.Write("/");
                    break;
                    case 1:
                    Console.Write("-");
                    break;
                    case 2:
                    Console.Write("\\");
                    break;
                    case 3:
                    Console.Write("|");
                    break;
                }
            
                Console.SetCursorPosition(Console.CursorLeft-1, Console.CursorTop);
                System.Threading.Thread.Sleep(150);
            }
        }
    }

}