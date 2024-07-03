namespace KKHS_API.helper
{
    public class ApiResult
    {
        public bool Success { set; get; }

        public string? Message { set; get; }

        public string? Token { set; get; }  
       
    }

    public class ApiDataResult<T> : ApiResult
    {
        public T? data { set; get; }

        public object? OValue { set; get; }

    }
}
