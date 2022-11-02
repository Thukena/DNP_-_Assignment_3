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
        Post post = await postDao.CreateAsync(dto);
        return post;
    }

    public Task<IEnumerable<Post>> GetAsync(PostSearchParametersDto dto)
    {
        return postDao.GetAsync(dto);
    }
}