using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.Dtos
{
    public class TaskReadDto : TaskCreateDto
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
    }
}