namespace HW2;

public static class UsersManager
{
    private static List<User?> Users{ get; set; } = new List<User?>();
        
    public static (bool Result, string Message) AddUser(string username, string password, int age)
    {
        if (Users.Find(u => u.Username == username) != null)
        {
            var newUser = new User(Users.Count + 1, username, age, password);
            Users.Add(newUser);
            return (true, $"User {username} added.");
        }
        return (false, $"User {username} already exist.");
    }
    
    public static (bool Result, string Message) EditUserName(string oldUsername, string newUsername)
    {
        var oldUser = Users.Find(u => u.Username == oldUsername);
        if (Users.Find(u => u.Username== newUsername) == null && oldUser!=null)
        {
            oldUser.Username = newUsername;
            return (true, $"User {oldUsername} updated.");
        }
        return (false, "User with this name already exist.");
    }
    
    public static (bool Result, string Message) DeleteUser(int userId)
    {
        User? userToRemove = Users.Find(user => user != null && user.Id == userId);
        if (userToRemove != null)
        {
            Users.Remove(userToRemove);
            return (true, $"User {userToRemove.Username} deleted.");
        }
        return (false, $"User does not exist.");
    }
    
    public static (bool Result, string Message) AutorizeUser(string username, string password)
    {   
        User user = Users.Find(u => u.Username == username);
        if (user.Password == password)
        {
            return (true, $"User {username} is autorized.");
        }
        
        return (false, $"Wrong password.");
    }
}
