using Application.DaoInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    
    private readonly ForumContext context;

    public PostEfcDao(ForumContext context)
    {
        this.context = context;
    }
    
    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await context.Users.FindAsync(dto.UserId);

        if (user != null)
        {
            EntityEntry<Post> post = await context.Posts.AddAsync(new Post(user,dto.Title,dto.Body));
            await context.SaveChangesAsync();
            return post.Entity;   
        }
        
        throw new Exception($"The User with id {dto.UserId} does not exist!");

    }

    public async Task<IEnumerable<Post>> GetAsync(PostSearchParametersDto dto)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.User).AsQueryable();
        
        if (!string.IsNullOrEmpty(dto.Title))
        {
            query = query.Where(post => post.Title.ToLower().Contains(dto.Title.ToLower()));
        }

        IEnumerable<Post> result = await query.ToListAsync();
        return result;


    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        Post? post = await context.Posts.Include(p => p.User).FirstOrDefaultAsync(post1 => post1.Id == id);
        return post;
    }
}