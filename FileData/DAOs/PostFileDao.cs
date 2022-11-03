using Application.DaoInterface;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = context.Users.FirstOrDefault(u => u.Id == dto.UserId);

        int id = 1;
        
        if (context.Posts.Any())
        {
            id = context.Posts.Max(p => p.Id);
            id++;
        }

        if (user != null)
        {
            Post post = new Post(id, user, dto.Title, dto.Body);
            context.Posts.Add(post);
            context.SaveChanges();

            return Task.FromResult(post);
        }
        
        throw new Exception($"The User with id {id} does not exist!");


    }

    public Task<IEnumerable<Post>> GetAsync(PostSearchParametersDto dto)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();

        if (!string.IsNullOrEmpty(dto.Title))
        {
            result = context.Posts.Where(post => post.Title.Contains(dto.Title, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

}