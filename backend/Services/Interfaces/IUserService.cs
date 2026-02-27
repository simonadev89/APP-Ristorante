using System.Threading.Tasks;
using Ristorante.Common;
using Ristorante.DTOs;

namespace Ristorante.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<object>> RegisterAsync(RegisterUserDto dto);
    }
}
