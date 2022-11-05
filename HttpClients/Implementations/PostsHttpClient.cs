using System.Text;
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
    
    public async Task CreateAsync(int userId, string title, string body)
    {
        PostCreationDto dto = new(userId, title, body);
        
        string postAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("/posts", content);

        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }    
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

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"posts/{id}");
        string result = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        PostBasicDto dto = JsonSerializer.Deserialize<PostBasicDto>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return dto;
    }
}