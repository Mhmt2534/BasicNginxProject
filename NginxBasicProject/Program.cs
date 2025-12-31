var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();


app.MapGet("/whoami", () =>
{
    var machineName = Environment.MachineName;
    return Results.Ok($"Hello from {machineName}");
});

app.MapGet("/health", () => Results.Ok("OK"));

app.MapGet("/scheme", (HttpContext ctx) =>
{
    return Results.Ok(new
    {
        Scheme = ctx.Request.Scheme,
        ForwardedProto = ctx.Request.Headers["X-Forwarded-Proto"].ToString()
    });
});

app.Run();
