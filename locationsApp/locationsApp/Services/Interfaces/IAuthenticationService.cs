using locationsApp.Models.Requests;
using System.Threading.Tasks;
using locationsApp.Models.Responses;

namespace locationsApp.Services.Interfaces
{
    public interface IAuthenticationService
	{
        Task<LoginResponse> AuthenticateAsync(LoginRequest LoginCredentials);
    }
}


