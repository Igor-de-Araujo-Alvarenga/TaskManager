using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Data
{
    public enum TaskStatus
    {
        Backlog = 1,
        ToDo,
        InProgress,
        InReview,
        Done,
        Cancelled
    }
}