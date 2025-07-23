using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<TodoContext>(opt => 
    opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddScoped<ITodoService, TodoService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Todo API", 
        Version = "v1",
        Description = "A simple Todo List API built with ASP.NET Core"
    });
});

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseCors("AllowFrontend");

// API Endpoints
app.MapGet("/api/todos", async (ITodoService todoService) =>
{
    var todos = await todoService.GetAllTodosAsync();
    return Results.Ok(todos.Select(t => new TodoDto
    {
        Id = t.Id,
        Title = t.Title,
        Description = t.Description,
        IsCompleted = t.IsCompleted,
        Priority = t.Priority,
        DueDate = t.DueDate,
        CreatedAt = t.CreatedAt,
        UpdatedAt = t.UpdatedAt
    }));
})
.WithName("GetTodos")
.WithTags("Todos")
.WithOpenApi();

app.MapGet("/api/todos/{id:int}", async (int id, ITodoService todoService) =>
{
    var todo = await todoService.GetTodoByIdAsync(id);
    if (todo == null)
        return Results.NotFound($"Todo with ID {id} not found.");
    
    return Results.Ok(new TodoDto
    {
        Id = todo.Id,
        Title = todo.Title,
        Description = todo.Description,
        IsCompleted = todo.IsCompleted,
        Priority = todo.Priority,
        DueDate = todo.DueDate,
        CreatedAt = todo.CreatedAt,
        UpdatedAt = todo.UpdatedAt
    });
})
.WithName("GetTodoById")
.WithTags("Todos")
.WithOpenApi();

app.MapPost("/api/todos", async (CreateTodoDto createDto, ITodoService todoService) =>
{
    var todo = new Todo
    {
        Title = createDto.Title,
        Description = createDto.Description,
        Priority = createDto.Priority,
        DueDate = createDto.DueDate,
        IsCompleted = false
    };
    
    var createdTodo = await todoService.CreateTodoAsync(todo);
    var todoDto = new TodoDto
    {
        Id = createdTodo.Id,
        Title = createdTodo.Title,
        Description = createdTodo.Description,
        IsCompleted = createdTodo.IsCompleted,
        Priority = createdTodo.Priority,
        DueDate = createdTodo.DueDate,
        CreatedAt = createdTodo.CreatedAt,
        UpdatedAt = createdTodo.UpdatedAt
    };
    
    return Results.Created($"/api/todos/{createdTodo.Id}", todoDto);
})
.WithName("CreateTodo")
.WithTags("Todos")
.WithOpenApi();

app.MapPut("/api/todos/{id:int}", async (int id, UpdateTodoDto updateDto, ITodoService todoService) =>
{
    var todo = new Todo
    {
        Title = updateDto.Title,
        Description = updateDto.Description,
        IsCompleted = updateDto.IsCompleted,
        Priority = updateDto.Priority,
        DueDate = updateDto.DueDate
    };
    
    var updatedTodo = await todoService.UpdateTodoAsync(id, todo);
    if (updatedTodo == null)
        return Results.NotFound($"Todo with ID {id} not found.");
    
    return Results.Ok(new TodoDto
    {
        Id = updatedTodo.Id,
        Title = updatedTodo.Title,
        Description = updatedTodo.Description,
        IsCompleted = updatedTodo.IsCompleted,
        Priority = updatedTodo.Priority,
        DueDate = updatedTodo.DueDate,
        CreatedAt = updatedTodo.CreatedAt,
        UpdatedAt = updatedTodo.UpdatedAt
    });
})
.WithName("UpdateTodo")
.WithTags("Todos")
.WithOpenApi();

app.MapPatch("/api/todos/{id:int}/toggle", async (int id, ITodoService todoService) =>
{
    var todo = await todoService.ToggleTodoStatusAsync(id);
    if (todo == null)
        return Results.NotFound($"Todo with ID {id} not found.");
    
    return Results.Ok(new TodoDto
    {
        Id = todo.Id,
        Title = todo.Title,
        Description = todo.Description,
        IsCompleted = todo.IsCompleted,
        Priority = todo.Priority,
        DueDate = todo.DueDate,
        CreatedAt = todo.CreatedAt,
        UpdatedAt = todo.UpdatedAt
    });
})
.WithName("ToggleTodoStatus")
.WithTags("Todos")
.WithOpenApi();

app.MapDelete("/api/todos/{id:int}", async (int id, ITodoService todoService) =>
{
    var deleted = await todoService.DeleteTodoAsync(id);
    if (!deleted)
        return Results.NotFound($"Todo with ID {id} not found.");
    
    return Results.NoContent();
})
.WithName("DeleteTodo")
.WithTags("Todos")
.WithOpenApi();

app.Run();
