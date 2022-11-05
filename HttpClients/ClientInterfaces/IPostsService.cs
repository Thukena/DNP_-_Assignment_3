using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostsService
{

    Task CreateAsync(int userId, string title, string body);
    Task<IEnumerable<Post>> GetAsync(string? title = null);
    Task<PostBasicDto> GetByIdAsync(int id);

}