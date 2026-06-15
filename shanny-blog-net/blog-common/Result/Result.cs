using blog_common.Constant;

namespace blog_common.Result
{
    public class Result<T>
    {
        public string Code { get; set; }
        public string Msg { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static Result<T> Success()
        {
            return new Result<T>
            {
                Code = ResultMsg.CODE_SUCCESS
            };
        }

        public static Result<T> Success(string msg)
        {
            return new Result<T>
            {
                Code = ResultMsg.CODE_SUCCESS,
                Msg = msg
            };
        }

        public static Result<T> Success(T? data)
        {
            return new Result<T>
            {
                Code = ResultMsg.CODE_SUCCESS,
                Data = data
            };
        }

        public static Result<T> Success(string msg, T? data)
        {
            return new Result<T>
            {
                Code = ResultMsg.CODE_SUCCESS,
                Msg = msg,
                Data = data
            };
        }

        public static Result<T> Error(string msg)
        {
            return new Result<T>
            {
                Code = ResultMsg.CODE_FAIL,
                Msg = msg
            };
        }
    }
}