using Application.DaoInterface;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }


    public Task<User> CreateAsync(UserCreationDto dto)
    {
        int id = 1;
        if (context.Users.Any())
        {
            id = context.Users.Max(u => u.Id);
            id++;
        }

        User user = new User(id, dto.Username, dto.Password);
        
        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    //login
    public Task<User> GetAsync(UserLoginDto dto)
    {
        User? user = context.Users.Find(u => u.Username.Equals(dto.Username, StringComparison.OrdinalIgnoreCase));

        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (!user.Password.Equals(dto.Password))
        {
            throw new Exception("Invalid password");
        }
        
        return Task.FromResult(user);
    }
}