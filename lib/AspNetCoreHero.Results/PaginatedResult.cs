using System;
using System.Collections.Generic;

namespace AspNetCoreHero.Results;

public class PaginatedResult<T> : Result
{
    public PaginatedResult(List<T> data,int statusCode)
    {
        Data = data;
        StatusCode = statusCode;
    }

    public PaginatedResult()
    {
        
    }
    public List<T> Data { get; set; }

    internal PaginatedResult(List<T> data, int statusCode, List<string> messages = null, long count = 0, int page = 1, int pageSize = 10)
    {
        Data = data;
        Page = page;
        StatusCode = statusCode;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
    }

    public static new PaginatedResult<T> Failure(List<string> messages, int statusCode)
    {
        return new PaginatedResult<T>(default, statusCode, messages);
    }

    public static PaginatedResult<T> Success(List<T> data, int statusCode) // Yeni Eklendi---------------
    {
        return new PaginatedResult<T>(data, statusCode);
    }

    public static PaginatedResult<T> Success(List<T> data, int statusCode, long count, int page, int pageSize)
    {
        return new PaginatedResult<T>(data, statusCode, null, count, page, pageSize);
    }

    public int Page { get; set; }

    public int TotalPages { get; set; }

    public long TotalCount { get; set; }

    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;
}
