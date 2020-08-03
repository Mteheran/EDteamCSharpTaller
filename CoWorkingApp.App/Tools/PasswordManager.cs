using System;

namespace CoWorkingApp.App.Tools
{
    public static class PasswordManager
    {
        public static string GetPassWord()
        {
            string paswordInput = "";

            while(true)
            {
                var keyPress = Console.ReadKey(true);
                
                if(keyPress.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(" ");
                    break;
                }
                else
                {
                    Console.Write("*");
                    paswordInput+= keyPress.KeyChar;
                }
            }

            return paswordInput;
        }
    }
}