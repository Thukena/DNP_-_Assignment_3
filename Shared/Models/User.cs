using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class User
{
     [Key]
     public int Id { get; private set; }
     public string Username { get; private set; }
     public string Password { get; private set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
    public User(){}

}