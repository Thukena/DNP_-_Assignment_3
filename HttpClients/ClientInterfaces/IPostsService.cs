using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostsService
{

    Task CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> GetAsync(string? title = null);
    Task<PostBasicDto> GetByIdAsync(int id);

}