using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Interfacez;
using AutoMapper;
using Infraestructure.Layer___Bank_Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Layer___Bank_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IBankAdminRepository _bankAdminRepository;
        private readonly IPasswordHasher _passwordHasher;

        public TokenController(IMapper mapper, IConfiguration configuration, IBankAdminRepository bankAdminRepository, IPasswordHasher passwordHasher)
        {
            _mapper = mapper;
            _configuration = configuration;
            _bankAdminRepository = bankAdminRepository;
            _passwordHasher = passwordHasher;
        }
        /// <summary>
        /// Here You will To Get Token for to Use the Crud Methods
        /// </summary>
        /// <param name="newAdmin"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Authentication([FromQuery]BankAccountDTO newAdmin)
        {
            var validation = await IsValidAdmin(newAdmin);

            if (validation.Item1) {
                var token = GenerateToken(validation.Item2);

                return Ok(new { token });
            }

            return NotFound();
        }

        private async Task<(bool, BankAccount)> IsValidAdmin(BankAccountDTO newAdmin)
        {
            var NewAdmin = await _bankAdminRepository.GetAdmin(newAdmin);
            var IsValid = _passwordHasher.Check(NewAdmin.BankPasswordAdmin, newAdmin.BankPasswordAdmin);
            return (IsValid, NewAdmin);
        }

        private string GenerateToken(BankAccount newAdmin)
        {
            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Authentication:SecretKey"]));

            var signingCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(signingCredentials);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, newAdmin.BankUserAdmin),
                new Claim(ClaimTypes.Email, "Anonimus@gmail-hotmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var payload = new JwtPayload(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(60)
                );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
