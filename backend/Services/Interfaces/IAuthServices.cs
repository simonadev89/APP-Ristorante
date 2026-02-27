using Ristorante.Common;
using Ristorante.DTOs;

namespace Ristorante.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<object>> LoginAsync(LoginDto dto);
    }
}