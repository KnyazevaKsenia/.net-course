namespace HW2;

public class User
{
    public User(int id, string username, int age, string password)
    {
        Id = id;
        Username = username;
        Age = age;
        Password = password;
    }
    public int Id { get; set; }
    public string Username { get; set; }
    public int Age { get; set; }
    public string Password { get; set; }
}