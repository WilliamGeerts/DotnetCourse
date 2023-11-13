using Infrastructure;
using Infrastructure.EntityFramework;
using Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Todo")));

// Add Injections
builder.Services.AddScoped<Database>();
builder.Services.AddScoped<TodoFactory>();

// SQL Server
//builder.Services.AddScoped<ITodoRepository, TodoRepository>();
//EntityFramewrok
builder.Services.AddScoped<ITodoRepository, EfTodoRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Dev", policyBuilder =>
    {
        policyBuilder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Dev");

app.UseAuthorization();

app.MapControllers();

app.Run();