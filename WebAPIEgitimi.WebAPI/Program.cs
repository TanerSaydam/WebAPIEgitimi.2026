var builder = WebApplication.CreateBuilder(args);

//DI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();
//Midleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
