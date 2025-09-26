using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Interface;
using TaskTracker.Model;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
using TaskTracker.Enums;
using Microsoft.VisualBasic;

namespace TaskTracker.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private static string fileName = "taskdata.json";
        private static string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        public async Task<UserTask?> AddTask(string description)
        {
            try
            {
                var task = new UserTask
                {
                    id = await CreateTaskId(),
                    description = description,
                    taskStatus = Enums.Status.todo,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                };

                var taskList = await GetTasksFromJson();
                
                taskList.Add(task);

                await UpdateJson(taskList);

                Console.WriteLine($"Task {task.description} created succesfully with ID {task.id}");

                return task;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error ocurred:" + ex);
                return null;
            }
        }

        private async Task<int> CreateTaskId()
        {
            var userTasks = await GetTasksFromJson();
            return userTasks.Count == 0 ? 1 : userTasks.Max(x => x.id) + 1;
        }
        public async Task<UserTask?> DeleteTask(int id)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            try
            {
                var userTasks = await GetTasksFromJson();

                var task = userTasks.FirstOrDefault(x => x.id == id);

                if (task == null)
                {
                    return null;
                }

                userTasks.Remove(task);

                await UpdateJson(userTasks);

                Console.WriteLine($"Task {task.description} succesfully deleted.");

                return task;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has ocurred: " + ex);
                return null;
            }

        }

        public async Task<List<UserTask>> GetAll()
        {

            var userTasks = await GetTasksFromJson();

            return userTasks ?? new List<UserTask>();

        }

        public async Task<List<UserTask>> GetByStatus(string status)
        {
            var userTasks = await GetTasksFromJson();

            if (Enum.TryParse<Status>(status, out var parsedStatus))
            {
                return userTasks.FindAll(x => x.taskStatus == parsedStatus);
            }

            return new List<UserTask>();
        }

        public List<string> HelpComands()
        {
            throw new NotImplementedException();
        }

        public async Task<UserTask?> SetStatus(int id, string status)
        {
            try
            {
                var userTasks = await GetTasksFromJson();

                if (userTasks == null || userTasks.Count() == 0)
                {
                    return null;
                }

                var task = userTasks.Find(x => x.id == id);

                if (task == null)
                {
                    return null;
                }

                if (Enum.TryParse<Status>(status, out var parsedStatus))
                {
                    task.taskStatus = parsedStatus;
                    await UpdateJson(userTasks);
                    return task;
                }
                else
                {
                    Console.WriteLine("Invalid Status");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured:" + ex);
                return null;
            }
        }

        public async Task<UserTask?> UpdateTask(int id, string description)
        {
            var userTasks = await GetTasksFromJson();

            if (userTasks == null || userTasks.Count() == 0)
            {
                return null;
            }

            var task = userTasks.Find(x => x.id == id);

            if (task == null)
            {
                return null;
            }

            task.description = description;

            await UpdateJson(userTasks);
            return task;
        }

        private static async Task<List<UserTask>> GetTasksFromJson()
        {
            if (!File.Exists(filePath))
            {
                return new List<UserTask>();
            }

            var fileContent = await File.ReadAllTextAsync(filePath);

            try
            {
                return JsonSerializer.Deserialize<List<UserTask>>(fileContent) ?? new List<UserTask>();
            }
            catch (JsonException)
            {
                Console.WriteLine("The file is corrupted or has an invalid format.");
                return new List<UserTask>();
            }
        }

        private static async Task UpdateJson(List<UserTask> userTasks)
        {
            var serializedList = JsonSerializer.Serialize(userTasks, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(filePath, serializedList);
        }
    }
}