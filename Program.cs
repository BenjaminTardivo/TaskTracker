using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Repository;

namespace TaskTracker
{
    internal class Program
    {

        static void Main(string[] args)
        {
            TaskRepository taskRepository = new TaskRepository();

            WelcomeMessage();

            while (true)
            {
                Console.ReadLine();
            }
        }

        static void WelcomeMessage()
        {
            Console.WriteLine("========== Welcome to TaskTracker app ==========\n");
            Console.WriteLine("type help to list all commands");
        }
    }
}