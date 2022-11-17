using Application.DaoInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{
    
    private readonly ForumContext context;

    public UserEfcDao(ForumContext context)
    {
        this.context = context;
    }
    
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User user = new(dto.Username, dto.Password);
        
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;    
    }

    public async Task<User> GetAsync(UserLoginDto dto)
    {
        User? user = await context.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(dto.Username.ToLower())
        );

        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (!user.Password.ToLower().Equals(dto.Password))
        {
            throw new Exception("Invalid password");
        }
        
        return user;    
    }
}