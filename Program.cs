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

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}