namespace Domain.Models;

public class Post
{
    public int Id { get; }
    public User User { get; }
    public string Title { get; }
    public string Body { get; }

    public Post(int id, User user, string title, string body)
    {
        Id = id;
        User = user;
        Title = title;
        Body = body;
    }
}