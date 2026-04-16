using System;

namespace HistoryService.Core.Entities
{
    public class HistoryEntry
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OperationType { get; set; } = string.Empty;
        public string InputValues { get; set; } = string.Empty;  // JSON format
        public string Result { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}