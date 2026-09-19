using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTO.Auth;
using TaskFlow.Application.IRepository;

namespace TaskFlow.Application.IService
{
    public interface IAuthService
    {
        public Task Register(RegisterDTO register);

        public Task<string> Login(LoginDTO login);
    }
}
