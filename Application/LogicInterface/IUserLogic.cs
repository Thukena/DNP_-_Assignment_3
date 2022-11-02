using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterface;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto dto);
    public Task<User> GetAsync(UserLoginDto dto);

}