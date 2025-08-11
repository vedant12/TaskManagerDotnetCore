using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Mappings;
using TaskManager.Core.Dtos;
using TaskManager.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/tasks", async (AppDbContext context) =>
{
    return await context.TaskItems.ToListAsync();
});

app.MapPost("/tasks", async (TaskCreateDto dto, AppDbContext context, IMapper mapper) =>
{
    var request = mapper.Map<TaskItem>(dto);

    bool taskCheck = context.TaskItems.FirstOrDefaultAsync(x => x.Title.Equals(dto.Title, StringComparison.CurrentCultureIgnoreCase)) != null;

    if (taskCheck) return Results.Conflict("Task with same name exists");

    await context.TaskItems.AddAsync(request);
    await context.SaveChangesAsync();
    return Results.Created("Created", dto);
});

app.MapPut("/tasks/{id:int}", async (int id, TaskCreateDto dto, AppDbContext context, IMapper mapper) =>
{
    var task = await context.TaskItems.FindAsync(id);
    //bool taskCheck = context.TaskItems.FirstOrDefaultAsync(x => x.Title.Equals(dto.Title, StringComparison.CurrentCultureIgnoreCase)) != null;

    if (task is null) return Results.NotFound();
    //if (taskCheck) return Results.Conflict("Task with same name exists");

    mapper.Map(dto, task);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/tasks/{id:int}", async (int id, AppDbContext context) =>
{
    var task = await context.TaskItems.FindAsync(id);
    if (task is null) return Results.NotFound();

    context.TaskItems.Remove(task);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();