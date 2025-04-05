namespace Hexata.BI.Application.Dtos
{
    public sealed record Result<TValue, TError>
    {
        public TValue? Value { get; set; }
        public TError? Error { get; set; }
        public bool IsSuccess { get; set; }

        public Result()
        {

        }

        private Result(TValue value)
        {
            IsSuccess = true;
            Value = value;
            Error = default;
        }

        private Result(TError error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        //happy path
        public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value);

        //error path
        public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error);

        public Result<TValue, TError> Match(Func<TValue, Result<TValue, TError>> success, Func<TError, Result<TValue, TError>> failure)
        {
            if (IsSuccess)
            {
                return success(Value!);
            }
            return failure(Error!);
        }
    }
}
