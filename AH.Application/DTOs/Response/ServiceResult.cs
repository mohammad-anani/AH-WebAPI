namespace AH.Application.DTOs.Response
{
    public class ServiceResult<T>
    {
        public T? Data { get; private set; }
        public int StatusCode { get; private set; }
        public string Message { get; private set; }

        private ServiceResult(T? data, int statusCode, string message)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
        }

        /// <summary>
        /// Creates a ServiceResult based on given data and optional exception.
        /// If exception is null, returns a success result (200).
        /// If exception is provided, maps it to the correct status code and message.
        /// </summary>
        public static ServiceResult<T> Create(T? data, Exception? ex = null, string successMessage = "Success")
        {
            if (ex is null)
            {
                return new ServiceResult<T>(data, 200, successMessage);
            }

            return ex switch
            {
                var e when e.GetType().Name == "SqlException" && e.Message.Contains("2627") =>
                    new ServiceResult<T>(default, 409, e.Message),

                var e when e.GetType().Name == "SqlException" =>
                    new ServiceResult<T>(default, 400, e.Message),

                InvalidOperationException e =>
                    new ServiceResult<T>(default, 400, e.Message),

                ArgumentNullException e =>
                    new ServiceResult<T>(default, 400, e.Message),
                KeyNotFoundException e =>
                    new ServiceResult<T>(default, 404, e.Message),

                _ =>
                    new ServiceResult<T>(default, 500, ex.Message)
            };
        }
    }
}