using System;

namespace ModelLayer.Exceptions
{
    /// <summary>
    /// Custom exception for database operations - UC16
    /// </summary>
    public class DatabaseException : Exception
    {
        public DatabaseException() { }
        
        public DatabaseException(string message) : base(message) { }
        
        public DatabaseException(string message, Exception innerException) 
            : base(message, innerException) { }
        
        public int? ErrorCode { get; set; }
    }
}
