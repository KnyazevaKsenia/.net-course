using Microsoft.AspNetCore.Mvc;

namespace HW2;

public static class UsersEndpointExt
{
    public static List<User> Users = new List<User>();
    public static IEndpointRouteBuilder MapUsersController(this IEndpointRouteBuilder builder)
    {
        var usersGroup = builder.MapGroup("users");
        usersGroup.MapPost("create",  ([FromBody] UserModel userModel) =>
        {
            if (Users.Any(x => x.Username == userModel.Username))
            {
                return Results.Conflict("User with this username already exists");
            }
            Users.Add(new User(Users.Count +1, userModel.Username, userModel.Age, userModel.Password));
            return Results.Ok();
        });

        usersGroup.MapPost("login", (HttpContext context, [FromBody] UserLoginModel loginModel) =>
        {
            var user = Users.FirstOrDefault(x => x.Username == loginModel.Username);
            if (user == null)
            {
                return Results.NotFound("User with this username does not exist");
            }

            if (user.Password != loginModel.Password)
            {
                return Results.Unauthorized();
            }
            return Results.Ok();
        });

        usersGroup.MapPost("edit/{id}", async (int id, [FromBody] UserModel user) =>
        {
            var userToUpdate = Users.FirstOrDefault(x => x.Id == id);
            if (userToUpdate == null)
            {
                return Results.NotFound("User with this id does not exist");
            }

            var isChanged = user.Username != userToUpdate.Username ||
                            user.Age != userToUpdate.Age ||
                            user.Password != userToUpdate.Password;
            
            if (isChanged)
            {
                userToUpdate.Username = user.Username;
                userToUpdate.Age = user.Age;
                userToUpdate.Password = user.Password;
    
                return Results.Ok();
            }
            
            return Results.Conflict("Nothing changed");
        });

        usersGroup.MapPost("delete/{id}", async (int id) =>
        {
            var userToDelete = Users.FirstOrDefault(x => x.Id == id);
            if (userToDelete == null)
            {
                return Results.NotFound("User with this id does not exist");
            }
            Users.Remove(userToDelete);
            return Results.Ok();
        });
        return usersGroup;
    }
}