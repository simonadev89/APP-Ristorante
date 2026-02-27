namespace Ristorante.Common
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ServiceResponse<T> Ok(T data, string message = null)
        {
            return new ServiceResponse<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }

        public static ServiceResponse<T> Fail(string message)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}
