using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterface;

public interface IPostLogic
{
    public Task<Post> CreateAsync(PostCreationDto dto);
    public Task<IEnumerable<Post>> GetAsync(PostSearchParametersDto dto);


}