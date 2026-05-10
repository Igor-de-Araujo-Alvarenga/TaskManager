using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Data
{
    public class Task
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TypeTask TypeTask { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Backlog;
        public long? ParentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public Task? Parent { get; set; }
        public ICollection<Task> Children { get; set; } = new List<Task>();
        public ICollection<TaskTag> Tags { get; set; } = new List<TaskTag>();
    }
}