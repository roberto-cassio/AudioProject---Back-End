using AudioProject___BackEnd.Context;
using AudioProject___BackEnd.Models;
using AudioProject___BackEnd.Response;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AudioProject___BackEnd.Repository
{
    public static class UserExtension
    {
        public static void AddUserRoutes(this WebApplication app)
        {
            var routeUser = app.MapGroup("users");

            routeUser.MapPost("", async (UserRequest request, AudioDbContext context) =>
            {
                var newUser = new User(request.Name, request.Email, request.Password);
                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();

                var userRequest = new UserRequest(newUser.Id, newUser.Email, newUser.Password, newUser.Name);

                return Results.Ok(userRequest);
            });

            routeUser.MapGet("", async (AudioDbContext context) =>
            {
                var user = await context.Users
                .Select(user => new UserRequest(user.Id, user.Name, user.Email, user.Password))
                .ToListAsync();
                return user;
            });


            routeUser.MapPut("{id}", async (int id, UserRequest request, AudioDbContext context) =>
            {
                var findUser = await context.Users.SingleOrDefaultAsync(user => user.Id == id);
                if (findUser == null)
                    return Results.NotFound();

                findUser.updateUserName(request.Name, request.Email, request.Password);

                await context.SaveChangesAsync();
                return Results.Ok(new UserResponse(findUser.Name, findUser.Email, findUser.Password));
            });


        }
    }
       
}
