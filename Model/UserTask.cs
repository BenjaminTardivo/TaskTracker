using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Enums;

namespace TaskTracker.Model
{
    public class UserTask
    {
        public int id { get; set; }
        public string description { get; set; } = string.Empty;
        public Status taskStatus { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}