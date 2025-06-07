using SistemaReservas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservas.Application.Interfaces
{
    public interface IAuthAppService
    {
        Task<string?> LoginAsync(string email, string password);
        Task<OperationResultDto<string>> RegistrarAsync(RegistrarUsuarioRequest request);
    }
}
