using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Model;
using TaskTracker.Repository;

namespace TaskTracker
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            TaskRepository taskRepository = new TaskRepository();

            if (args.Length == 0)
            {
                Console.WriteLine("No command provided. Use 'help' to see available commands.");
                return;
            }

            var command = args[0].ToLower();
            var param = args.Skip(1).ToArray();

            switch (command)
            {
                case "help":
                    var commands = taskRepository.HelpComands();
                    PrintHelpCommands(commands);
                    break;

                case "add":
                    if (param.Length == 0)
                    {
                        Console.WriteLine("Usage: task-cli add <description>");
                        return;
                    }

                    var description = string.Join(" ", param);

                    await taskRepository.AddTask(description);
                    break;

                case "update":
                    if (param.Length < 2 || !int.TryParse(param[0], out var updId))
                    {
                        Console.WriteLine("Usage: task-cli update <id> <new description>");
                        return;
                    }

                    var updated = await taskRepository.UpdateTask(updId, string.Join(" ", param.Skip(1)));
                    Console.WriteLine(updated != null ? $"Task {updated.description} updated" : "Task not found");

                    break;

                case "delete":
                    if (param.Length < 1 || !int.TryParse(param[0], out var delId))
                    {
                        Console.WriteLine("Usage: task-cli delete <id>");
                        return;
                    }

                    var deleted = await taskRepository.DeleteTask(delId);
                    Console.WriteLine(deleted != null ? $"Task {deleted.description} deleted" : "Task not found");

                    break;

                case "mark-in-progress":
                case "mark-done":
                    if (param.Length < 1 || !int.TryParse(param[0], out var markId))
                    {
                        Console.WriteLine($"Usage: task-cli {command} <id>");
                        return;
                    }

                    string status = command == "mark-done" ? "done" : "in-progress";
                    var statusUpdated = await taskRepository.SetStatus(markId, status);

                    Console.WriteLine(statusUpdated != null
                        ? $"Task {markId} marked as {status}"
                        : "Task not found");

                    break;

                case "list":
                    if (param.Length == 0)
                    {
                        var allTasks = await taskRepository.GetAll();
                        PrintTask(allTasks);
                    }
                    else
                    {
                        var tasks = await taskRepository.GetByStatus(param[0]);
                        PrintTask(tasks);
                    }
                    break;

                default:
                    Console.WriteLine("Unknown Command. Use 'help' to list all commands");
                    break;





            }
        }

        private static void PrintTask(List<UserTask> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (var t in tasks)
            {
                Console.WriteLine($"[{t.id}] {t.description} - {t.taskStatus} (created {t.createdAt})");
            }
        }

        private static void PrintHelpCommands(List<String> commands)
        {
            foreach (var c in commands)
            {
                Console.WriteLine(c);
            }
        }

    }
}