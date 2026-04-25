using System.Threading.Tasks;

namespace AspNetCoreHero.Results;

public class Result : IResult
{
    public Result()
    {
    }

    public string Message { get; set; }

    public int StatusCode { get; set; }

    public static IResult Fail(int statusCode)
    {
        return new Result { StatusCode = statusCode };
    }

    public static IResult Fail(string message, int statusCode)
    {
        return new Result { StatusCode = statusCode, Message = message };
    }

    public static Task<IResult> FailAsync(int statusCode)
    {
        return Task.FromResult(Fail(statusCode));
    }

    public static Task<IResult> FailAsync(string message, int statusCode)
    {
        return Task.FromResult(Fail(message, statusCode));
    }

    public static IResult Success(int statusCode)
    {
        return new Result { StatusCode = statusCode };
    }

    public static IResult Success(string message, int statusCode)
    {
        return new Result { StatusCode = statusCode, Message = message };
    }

    public static Task<IResult> SuccessAsync(int statusCode)
    {
        return Task.FromResult(Success(statusCode));
    }

    public static Task<IResult> SuccessAsync(string message, int statusCode)
    {
        return Task.FromResult(Success(message, statusCode));
    }
}

public class Result<T> : Result, IResult<T>
{
    public Result()
    {
    }

    public T Data { get; set; }

    public static new Result<T> Fail(int statusCode)
    {
        return new Result<T> { StatusCode = statusCode };
    }

    public static new Result<T> Fail(string message, int statusCode)
    {
        return new Result<T> { StatusCode = statusCode, Message = message };
    }

    public static new Task<Result<T>> FailAsync(int statusCode)
    {
        return Task.FromResult(Fail(statusCode));
    }

    public static new Task<Result<T>> FailAsync(string message, int statusCode)
    {
        return Task.FromResult(Fail(message, statusCode));
    }

    public static new Result<T> Success(int statusCode)
    {
        return new Result<T> { StatusCode = statusCode };
    }

    public static new Result<T> Success(string message, int statusCode)
    {
        return new Result<T> { StatusCode = statusCode, Message = message };
    }

    public static Result<T> Success(T data, int statusCode)
    {
        return new Result<T> { StatusCode = statusCode, Data = data };
    }

    public static Result<T> Success(T data, string message, int statusCode)
    {
        return new Result<T> { StatusCode = statusCode, Data = data, Message = message };
    }

    public static new Task<Result<T>> SuccessAsync(int statusCode)
    {
        return Task.FromResult(Success(statusCode));
    }

    public static new Task<Result<T>> SuccessAsync(string message, int statusCode)
    {
        return Task.FromResult(Success(message, statusCode));
    }
  
    public static Task<Result<T>> SuccessAsync(T data, int statusCode)
    {
        return Task.FromResult(Success(data, statusCode));
    }

    public static Task<Result<T>> SuccessAsync(T data, string message, int statusCode)
    {
        return Task.FromResult(Success(data, message, statusCode));
    }
}
