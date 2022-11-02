namespace Domain.Models;

public class User
{
    
    public int Id { get; }
    public string Username { get; }
    public string Password { get; }

    public User(int id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }
}