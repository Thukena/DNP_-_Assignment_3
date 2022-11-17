﻿using Application.DaoInterface;
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

        if (string.IsNullOrEmpty(dto.Title))
        {
            throw new Exception("Title cannot be empty");
        }

        if (string.IsNullOrEmpty(dto.Body))
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

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return post;
    }
}