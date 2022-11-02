namespace Domain.DTOs;

public class PostSearchParametersDto
{
    public string? Title { get; }

    public PostSearchParametersDto(string? title)
    {
        Title = title;
    }
}