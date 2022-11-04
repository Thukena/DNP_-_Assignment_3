using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterface;

public interface IPostDao
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> GetAsync(PostSearchParametersDto dto);
    Task<Post?> GetByIdAsync(int id);

}