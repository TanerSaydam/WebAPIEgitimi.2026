using WebAPIEgitimi._3DersWebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<MyExceptionHandler>().AddProblemDetails();

builder.Services.AddCors(x =>
{
    x.AddPolicy("Policy1", h =>
    {

    });

    x.AddPolicy("Policy2", h =>
    {

    });

    x.AddPolicy("Policy3", h =>
    {

    });

    x.AddDefaultPolicy(h =>
    {

    });
});

var app = builder.Build();

//app.UseCors(x => x
//    .WithMethods("GET","POST","PUT")
//    .WithOrigins("https://www.tanersaydam.net", "https://www.tanersaydam.com")
//    .WithHeaders("Content-Type", "Authorization")
//);

//app.UseCors(x => x
//    .AllowAnyMethod()
//    .AllowAnyOrigin()
//    .AllowAnyHeader()

//    .SetPreflightMaxAge(TimeSpan.FromMinutes(10))
//);

app.UseCors(x => x
    .AllowAnyMethod()
    .SetIsOriginAllowed(i => true)
    .AllowAnyHeader()
    .AllowCredentials()

    .SetPreflightMaxAge(TimeSpan.FromMinutes(10))
);

app.UseStaticFiles();

app.UseExceptionHandler();

//app.Use(async (context, next) =>
//{
//    try
//    {
//        await next(context);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex);
//        context.Response.StatusCode = 400;
//        await context.Response.WriteAsync(ex.Message);
//    }
//});

app.MapGet("/", () =>
{
    int x = 0;
    int y = 0;
    if (x + y == 0)
        throw new TanerException("This is a custom exception thrown by Taner.");

    int z = x / y;
    //try
    //{
    //    int x = 0;
    //    int y = 0;
    //    int z = x / y;
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine(ex);
    //    return Results.BadRequest("Hello world!");
    //    //throw;
    //}
    //throw new Exception("bak bu böyle olmaz!");

    return Results.Ok("Hello world!");
});

app.Run();