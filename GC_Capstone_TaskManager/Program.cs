using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GC_Capstone_TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> tasks = Task.InstantiateDefaultTaskList();
            Console.WriteLine("Welcome to Task Manager");
            Thread.Sleep(1000);
            bool loop;
            do
            {
                loop = Task.PrintMenu(tasks);
            } while (loop);
        }
    }
}
