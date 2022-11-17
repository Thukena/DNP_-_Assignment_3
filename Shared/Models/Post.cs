using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class Post
{
    [Key]
    public int Id { get; set; }
    public User User { get; set;}
    public string Title { get; private set;}
    public string Body { get; private set;}

    public Post(User user, string title, string body)
    {
        User = user;
        Title = title;
        Body = body;
    }
    
    private Post(){}

}