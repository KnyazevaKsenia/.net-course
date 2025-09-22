using HW2;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapUsersController();

app.Run();

