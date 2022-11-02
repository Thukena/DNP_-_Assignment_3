using Domain.Models;

namespace FileData;

public class DataContainer
{
    public List<User> Users { get; set; }
    public List<Post> Posts { get; set; }

    public DataContainer()
    {
        Users = new List<User>();
        Posts = new List<Post>();
    }
}