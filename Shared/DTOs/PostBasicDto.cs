namespace Domain.DTOs;

public class PostBasicDto
{
    public int Id { get; }
    public string Username { get; }
    public string Title { get; }
    public string Body { get; }

    public PostBasicDto(int id, string username, string title, string body)
    {
        Id = id;
        Username = username;
        Title = title;
        Body = body;
    }
}