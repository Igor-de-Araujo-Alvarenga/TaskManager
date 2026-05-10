using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Data
{
    public class TaskTag
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long TaskId { get; set; }
        public Task TaskItem { get; set; } = null!;
    }
}