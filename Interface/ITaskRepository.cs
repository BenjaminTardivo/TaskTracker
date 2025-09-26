using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Model;

namespace TaskTracker.Interface
{
    public interface ITaskRepository
    {
        Task<UserTask?> AddTask(string description);
        Task<UserTask?> UpdateTask(int id, string description);
        Task<UserTask?> DeleteTask(int id);
        Task<UserTask?> SetStatus(int id, string status);
        Task<List<UserTask>> GetAll();
        Task<List<UserTask>> GetByStatus(string status);
        List<String> HelpComands();
    }
}