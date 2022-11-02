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
        User user = await userDao.CreateAsync(dto);
        return user;
    }

    public async Task<User> GetAsync(UserLoginDto dto)
    {
        Console.WriteLine("TESTSETST");
        User? user = await userDao.GetAsync(dto);

        if (user != null)
        {
            return user;
        }
        throw new Exception($"User {dto.Username} not found");
    }
}