using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Domain.DTOs
{
    public record RegisterDTO(string FullName, string Email, string Password);

}