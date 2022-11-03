using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostsHttpClient : IPostsService
{
    
    private readonly HttpClient _client;

    public PostsHttpClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task CreateAsync(PostCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Post>> GetAsync(string? title)
    {
        string uri = "/posts";
        
        if (!string.IsNullOrEmpty(title))
        {
            uri += $"?title={title}";
        }
        
        HttpResponseMessage response = await _client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        IEnumerable<Post> posts = JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return posts;
        
    }
}