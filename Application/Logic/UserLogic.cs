using Application.DaoInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        //todo add validation
        if (string.IsNullOrEmpty(dto.Username) ||string.IsNullOrEmpty(dto.Password))
        {
            throw new Exception($"Username or password cannot be empty!");
        }

        User user = await userDao.CreateAsync(dto);
        
        return user;
        
    }

    public async Task<User> GetAsync(UserLoginDto dto)
    {
        User? user = await userDao.GetAsync(dto);

        if (user != null)
        {
            return user;
        }
        throw new Exception($"User {dto.Username} not found");
    }
}