using System;
using CoWorkingApp.Data;

namespace CoWorkingApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var  userData = new UserData();
            userData.CreateAdmin();
            Console.WriteLine("Hello World!");
        }
    }
}
