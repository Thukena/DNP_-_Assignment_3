using Application.DaoInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    
    private readonly IPostDao postDao;

    public PostLogic(IPostDao postDao)
    {
        this.postDao = postDao;
    }
    
    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        //todo add validation

        if (dto.Title == null)
        {
            throw new Exception("Title cannot be empty");
        }

        if (dto.Body == null)
        {
            throw new Exception("Body cannot be empty");
        }
        
        Post post = await postDao.CreateAsync(dto);
        return post;
    }

    public Task<IEnumerable<Post>> GetAsync(PostSearchParametersDto dto)
    {
        return postDao.GetAsync(dto);
    }

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Todo with id {id} not found");
        }

        return new PostBasicDto(post.Id, post.User.Username, post.Title, post.Body);
    }
}