using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterface;

public interface IUserDao
{
    public Task<User> CreateAsync(UserCreationDto dto);
    public Task<User> GetAsync(UserLoginDto dto);

}