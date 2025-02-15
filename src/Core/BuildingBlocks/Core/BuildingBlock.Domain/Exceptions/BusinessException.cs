namespace BuildingBlock.Domain.Exceptions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly", Justification = "<Pendente>")]
    public class BusinessException : Exception
    {
        public string KeyError { get; private set; }

        public BusinessException(string? message) : base(message)
        {
            this.KeyError = string.Empty;
        }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
            this.KeyError = string.Empty;
        }

        public BusinessException(string? message, string keyError, Exception? innerException) : base(message, innerException)
        {
            this.KeyError = keyError;
        }
    }
}
