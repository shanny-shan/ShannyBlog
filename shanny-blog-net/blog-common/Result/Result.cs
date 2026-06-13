using blog_common.Enums;

namespace blog_common.Result
{
    public class Result<T>
    {
        public ResultCode Code { get; set; }
        public string Msg { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static Result<T> Success()
        {
            return new Result<T>
            {
                Code = ResultCode.Success
            };
        }

        public static Result<T> Success(string msg)
        {
            return new Result<T>
            {
                Code = ResultCode.Success,
                Msg = msg
            };
        }

        public static Result<T> Success(T? data)
        {
            return new Result<T>
            {
                Code = ResultCode.Success,
                Data = data
            };
        }

        public static Result<T> Success(string msg, T? data)
        {
            return new Result<T>
            {
                Code = ResultCode.Success,
                Msg = msg,
                Data = data
            };
        }

        public static Result<T> Error(string msg)
        {
            return new Result<T>
            {
                Code = ResultCode.Error,
                Msg = msg
            };
        }
    }
}
