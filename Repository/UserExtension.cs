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
            });

            routeUser.MapGet("", async (AudioDbContext context) =>
            {
                var user = await context.Users.ToListAsync();
            });


            routeUser.MapPut("{id}", async (int id, UserRequest request, AudioDbContext context) =>
            {
                var findUser = await context.Users.SingleOrDefaultAsync(user => user.Id == id);
                if (findUser == null)
                    return Results.NotFound();

                findUser.updateUserName(request.Name);

                await context.SaveChangesAsync();
                return Results.Ok(findUser);
            });
        }
    }
       
}
