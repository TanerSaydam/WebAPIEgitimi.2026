using WebAPIEgitimi._3DersWebAPI;

var builder = WebApplication.CreateBuilder(args);

#region Önceki Konular
builder.Configuration.AddJsonFile("taner.json", optional: true, reloadOnChange: true);

var value = builder.Configuration.GetSection("key-taner").Value;

//Console.WriteLine(value);
builder.Services.AddExceptionHandler<MyExceptionHandler>().AddProblemDetails();

//builder.Services.AddTransient<ProductService>();

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

builder.Services.Configure<Test>(builder.Configuration.GetSection("test"));
#endregion


builder.Services.AddResponseCompression(x => x.EnableForHttps = true);

var app = builder.Build();

#region CORS
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
#endregion

app.UseResponseCompression();

app.MapGet("/", () =>
{
    List<string> list = new();
    for (int i = 0; i < 1000000; i++)
    {
        list.Add($"Names {i}");
    }

    return list;
});

#region Eski Endpoint
//app.MapGet("/", (IOptionsMonitor<Test> options, IConfiguration configuration) =>
//{
//    var res2 = configuration.GetSection("test:Key").Value;
//    var res = options.CurrentValue;

//    return res;
//});

//app.MapGet("/", (IServiceProvider serviceProvider) =>
//{

//    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

//    ProductService productService = new(configuration);

//    var res = productService;
//    //int x = 0;
//    //int y = 0;
//    //if (x + y == 0)
//    //    //throw new TanerException("This is a custom exception thrown by Taner.");

//    //int z = x / y;
//    //try
//    //{
//    //    int x = 0;
//    //    int y = 0;
//    //    int z = x / y;
//    //}
//    //catch (Exception ex)
//    //{
//    //    Console.WriteLine(ex);
//    //    return Results.BadRequest("Hello world!");
//    //    //throw;
//    //}
//    //throw new Exception("bak bu böyle olmaz!");

//    return Results.Ok(res);
//});
#endregion


app.Run();