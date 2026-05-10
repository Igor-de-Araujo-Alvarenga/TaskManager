using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Domain.DTOs
{
    public class DefaultResponseDTO<T>
    {
        public T? Content { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }

        public static DefaultResponseDTO<T> Ok(T content, string message = "Success") =>
            new() { Content = content, Message = message, Success = true };

        public static DefaultResponseDTO<T> Fail(string message) =>
            new() { Content = default, Message = message, Success = false };
    }
}