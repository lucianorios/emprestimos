using AutoMapper;
using Loan.WebApi.ApplicationServices.Base;
using Loan.WebApi.ApplicationServices.Services.Interfaces;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Domain.Repositories;
using Loan.WebApi.Extensions.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.Services.Impl
{
    public class UsuarioService: BaseService, IUsuarioService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(INotificationHandler<DomainNotification> domainNotifications, IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper,
            IConfiguration configuration, IUsuarioRepository usuarioRepository)
            : base(domainNotifications, unitOfWork, mediator, mapper)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<AuthorizationViewModel> Login(UserViewModel userModel)
        {
            var user = await _usuarioRepository.GetByLogin(userModel.Login, userModel.Password.Encrypt());

            if (user == null)
            {
                base.AddNotification("Usuário não encontrado");
                return null;
            }
            else
            {
                return GenerateToken(user);
            }
        }

        private List<Claim> GetClaims(Usuario usuario)
        {
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("claim1", "1"));
            permClaims.Add(new Claim("claim2", "2"));
            permClaims.Add(new Claim("login", usuario.Login));

            return permClaims;
        }

        private AuthorizationViewModel GenerateToken(Usuario usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppKey")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(
                claims: GetClaims(usuario),
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthorizationViewModel() {
                Token = jwt_token,
                UserName = usuario.Login
            };
        }
    }
}
