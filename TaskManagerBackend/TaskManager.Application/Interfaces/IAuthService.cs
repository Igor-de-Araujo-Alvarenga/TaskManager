using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Domain.DTOs;

namespace TaskManager.Application.Interfaces
{
    public interface IAuthService
    {
        Task<DefaultResponseDTO<string>> RegisterAsync(RegisterDTO dto);
        Task<DefaultResponseDTO<string>> LoginAsync(LoginDTO dto);
    }
}