using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterface;

public interface IPostDao
{
    public Task<Post> CreateAsync(PostCreationDto dto);
    public Task<IEnumerable<Post>> GetAsync(PostSearchParametersDto dto);


}