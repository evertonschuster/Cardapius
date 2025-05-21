namespace Hexata.BI.Application.Dtos
{
    public sealed class Result<TValue, TError>
    {
        public TValue? Value { get; set; }
        public TError? Error { get; set; }
        public bool IsSuccess { get; set; }

        public Result()
        {

        }

        public static Result<TValue, TError> WithError(TError error, TValue? value = default)
        {
            return new Result<TValue, TError>()
            {
                Error = error,
                Value = value,
                IsSuccess = false
            };
        }

        public static Result<TValue, TError> WithSuccess(TValue value, TError? error = default)
        {
            return new Result<TValue, TError>()
            {
                Value = value,
                Error = error,
                IsSuccess = true
            };
        }
    }
}
