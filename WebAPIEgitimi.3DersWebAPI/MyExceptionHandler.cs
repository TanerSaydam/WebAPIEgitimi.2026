using Microsoft.AspNetCore.Diagnostics;

namespace WebAPIEgitimi._3DersWebAPI;

public sealed class MyExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken cancellationToken)
    {
        context.Response.StatusCode = 400;

        if (ex.GetType() == typeof(TanerException))
        {
            context.Response.StatusCode = 422;
        }

        //result pattern giriş
        var res = new
        {
            Message = ex.Message,
            StatusCode = context.Response.StatusCode,
            IsSuccessful = false
        };

        await context.Response.WriteAsJsonAsync(res);
        return true;
    }
}

public sealed class TanerException : Exception
{
    public TanerException() : base("This is a empty custom exception thrown by Taner.")
    {

    }
    public TanerException(string msg) : base(msg)
    {
    }
}