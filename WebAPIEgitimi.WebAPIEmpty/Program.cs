using Carter;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

#region DI - Container / Service Collection / Service Registration
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
#endregion

var app = builder.Build();

#region middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(x =>
{
    //x.WithOrigins("https://localhost:4200", "https://localhost:4200");
    x.AllowAnyOrigin(); //hiç kısıtlama yok
    //x.WithMethods("GET", "POST");
    x.AllowAnyMethod(); //hiç kısıtlama yok
    //x.WithHeaders("Content-Type", "Authorization"); //sadece
    x.AllowAnyHeader();
});
app.MapControllers();

app.MapProducts();

app.MapCarter();

app.Run();

#endregion
