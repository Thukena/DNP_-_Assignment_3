using System.Security.Claims;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task RegisterAsync(string username, string password);
    public Task<ClaimsPrincipal> GetAuthAsync();

    
}