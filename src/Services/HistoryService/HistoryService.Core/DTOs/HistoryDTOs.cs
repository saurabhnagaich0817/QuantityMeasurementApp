using System;

namespace HistoryService.Core.DTOs
{
    // Save History Request
    public class SaveHistoryRequest
    {
        public string OperationType { get; set; } = string.Empty;
        public string InputValues { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
    }

    // History Response
    public class HistoryResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OperationType { get; set; } = string.Empty;
        public string InputValues { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    // API Response Wrapper
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public string? ErrorCode { get; set; }
    }

    // Delete Response
    public class DeleteResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int DeletedCount { get; set; }
    }
}