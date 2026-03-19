using AIDebateAPI.SignalR;
using Debate_Library;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

//Injection
builder.Services.AddScoped<IPersonFactory, PersonFactory>();
builder.Services.AddScoped<Random, Random>();

var app = builder.Build();

app.MapHub<DebateHub>("/hubs/debate");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Simple API key gate - checks every request before it reaches a controller or hub.
// We need to support two ways of sending the key because browsers/SignalR can't
// attach custom headers to WebSocket connections, so those go in the query string instead.
app.Use(async (context, next) =>
{
    var validKey = builder.Configuration["ApiKey"];

    // Regular HTTP requests send the key as a header
    var headerKey = context.Request.Headers["X-Api-Key"].ToString();
    // SignalR / WebSocket connections send it as ?apiKey=... since headers aren't available
    var queryKey = context.Request.Query["apiKey"].ToString();

    if (headerKey != validKey && queryKey != validKey)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
        return;
    }

    await next(); // key checks out, carry on
});

app.UseAuthorization();

app.MapControllers();

app.Run();
