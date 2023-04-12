namespace CarWebApi.Meddleware
{
    /// <summary>
    /// Расширения CustomExceptionHandlerMiddleware для включения в конвейер
    /// </summary>
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Расширения CustomExceptionHandlerMiddleware для включения в конвейер
        /// </summary>
        public static IApplicationBuilder UseCustomExceptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
