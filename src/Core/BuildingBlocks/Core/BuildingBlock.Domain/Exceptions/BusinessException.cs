namespace BuildingBlock.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        public string KeyError { get; }

        public Guid ErrorId { get; }

        public DateTimeOffset ErrorAt { get; }


        public BusinessException(string? message) : base(message)
        {
            this.KeyError = string.Empty;
            ErrorId = Guid.CreateVersion7();
            ErrorAt = DateTimeOffset.UtcNow;
        }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
            this.KeyError = string.Empty;
            ErrorId = Guid.CreateVersion7();
            ErrorAt = DateTimeOffset.UtcNow;
        }

        public BusinessException(string? message, string keyError, Exception? innerException) : base(message, innerException)
        {
            this.KeyError = keyError;
            ErrorId = Guid.CreateVersion7();
            ErrorAt = DateTimeOffset.UtcNow;
        }
    }
}
