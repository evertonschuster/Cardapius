namespace BuildingBlock.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        public string KeyError { get; private set; }

        public Guid ErrorCode { get; set; }

        public BusinessException(string? message) : base(message)
        {
            this.KeyError = string.Empty;
            ErrorCode = Guid.CreateVersion7();
        }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
            this.KeyError = string.Empty;
            ErrorCode = Guid.CreateVersion7();
        }

        public BusinessException(string? message, string keyError, Exception? innerException) : base(message, innerException)
        {
            this.KeyError = keyError;
            ErrorCode = Guid.CreateVersion7();
        }
    }
}
