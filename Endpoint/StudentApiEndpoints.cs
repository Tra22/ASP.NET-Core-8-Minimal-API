using Microsoft.AspNetCore.Mvc;
using MinimalAPIProject.Dto.Request;
using MinimalAPIProject.Repository;
using MinimalAPIProject.Service;

namespace MinimalAPIProject.Endpoint;
public static class StudentApiEndpoint 
{
    public static IServiceCollection AddStudentApi(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentService, StudentService>();

        return services;
    }
    public static IEndpointRouteBuilder MapStudentApiRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/api/students");
        {
            // Get all Student items
            _ = group.MapGet("", async ([FromServices] IStudentService studentService) =>
                {
                    var results = await studentService.GetAllStudents();
                    return Results.Ok(results);
                });

            // Get a specific Student item
            _ = group.MapGet("/{id}", async ([FromRoute] Guid id, [FromServices] IStudentService studentService) =>
                {
                    var model = await studentService.GetStudentById(id);
                    return model is null ? Results.Problem("Item not found.", statusCode: StatusCodes.Status404NotFound) : Results.Json(model);
                })
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);

            // Create a new Student item
            _ = group.MapPost("/", async ([FromBody] CreateStudentRequestDto createStudentRequestDto, [FromServices] IStudentService studentService) =>
                {
                    var student = await studentService.CreateStudent(createStudentRequestDto);
                    return Results.Created($"/api/students/{student.Id}", student);
                })
                .Produces(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest);

            // Mark a Student item as completed
            _ = group.MapPut("/{id}", async ([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto, [FromServices] IStudentService studentService) =>
                {
                    bool? isUpdated = await studentService.UpdateStudent(id, updateStudentRequestDto);

                    return isUpdated switch
                    {
                        true => Results.NoContent(),
                        false => Results.Problem("Item already completed.", statusCode: StatusCodes.Status400BadRequest),
                        _ => Results.Problem("Item not found.", statusCode: StatusCodes.Status404NotFound),
                    };
                })
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound);

            // Delete a Student item
            _ = group.MapDelete("/{id}", async ([FromRoute] Guid id, [FromServices] IStudentService studentService) =>
                {
                    var wasDeleted = await studentService.DeleteStudent(id);
                    return wasDeleted ? Results.NoContent() : Results.Problem("Item not found.", statusCode: StatusCodes.Status404NotFound);
                })
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound);
        }

        return builder;
    }
}