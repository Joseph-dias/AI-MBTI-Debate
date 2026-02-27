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
builder.Services.AddScoped<AIDebateHandler, AIDebateHandler>();

var app = builder.Build();

app.MapHub<DebateHub>("/hubs/debate");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
