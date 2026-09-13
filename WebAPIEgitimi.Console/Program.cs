//var builder = WebApplication.CreateBuilder();

//// Dependency Injection

//var app = builder.Build();



//// Middleware

//app.Run();


HttpClient httpClient = new();
var message = await httpClient.GetAsync("https://localhost:5000/api/products");

var res = await message.Content.ReadAsStringAsync();

Console.WriteLine(res);

Console.ReadLine();