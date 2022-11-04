using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterface;

public interface IPostLogic
{
     Task<Post> CreateAsync(PostCreationDto dto);
     Task<IEnumerable<Post>> GetAsync(PostSearchParametersDto dto);
     Task<PostBasicDto> GetByIdAsync(int id);


}