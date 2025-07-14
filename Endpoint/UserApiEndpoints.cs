using Microsoft.AspNetCore.Mvc;
using MinimalAPIProject.Dto.Request;
using MinimalAPIProject.Repository;
using MinimalAPIProject.Service;

namespace MinimalAPIProject.Endpoint;
public static class UserApiEndpoint
{
    public static IServiceCollection AddUserApi(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
    public static IEndpointRouteBuilder MapUserApiRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/api/users");
        {
            // Get all User items
            _ = group.MapGet("", async ([FromServices] IUserService userService) =>
                {
                    var results = await userService.GetAllUsers();
                    return Results.Ok(results);
                });

            // Get a specific User item
            _ = group.MapGet("/{id}", async ([FromRoute] Guid id, [FromServices] IUserService userService) =>
                {
                    var model = await userService.GetUserById(id);
                    return model is null ? Results.Problem("Item not found.", statusCode: StatusCodes.Status404NotFound) : Results.Json(model);
                })
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);

            // Create a new User item
            _ = group.MapPost("/", async ([FromBody] CreateUserRequestDto createUserRequestDto, [FromServices] IUserService userService) =>
                {
                    var user = await userService.CreateUser(createUserRequestDto);
                    return Results.Created($"/api/users/{user.Id}", user);
                })
                .Produces(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest);

            // Mark a User item as completed
            _ = group.MapPut("/{id}", async ([FromRoute] Guid id, [FromBody] UpdateUserRequestDto updateUserRequestDto, [FromServices] IUserService userService) =>
                {
                    bool? isUpdated = await userService.UpdateUser(id, updateUserRequestDto);

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

            // Delete a User item
            _ = group.MapDelete("/{id}", async ([FromRoute] Guid id, [FromServices] IUserService userService) =>
                {
                    var wasDeleted = await userService.DeleteUser(id);
                    return wasDeleted ? Results.NoContent() : Results.Problem("Item not found.", statusCode: StatusCodes.Status404NotFound);
                })
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound);
        }

        return builder;
    }
}
