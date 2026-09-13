using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

#region DI - Container / Service Collection / Service Registration
builder.Services.AddControllers();
//builder.Services.AddExtensionsTestMethod();
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApi();
#endregion

var app = builder.Build();

#region middleware

app.MapOpenApi();
app.MapScalarApiReference();

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

app.Run();
#endregion

#region notes and others
//controller endpoint
//minimal endpoint


static class Extensions
{
    public static void AddExtensionsTestMethod(this IServiceCollection services)
    {
    }
}
#endregion
