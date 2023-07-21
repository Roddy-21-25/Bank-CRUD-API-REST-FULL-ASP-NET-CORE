using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Interfacez;
using AutoMapper;
using Infraestructure.Layer___Bank_Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Presentation.Layer___Bank_Api.Responses;

namespace Presentation.Layer___Bank_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBankAdminRepository _bankAdminRepository;
        private readonly IPasswordHasher _passwordHasher;
        public SignUpController(IMapper mapper, IBankAdminRepository bankAdminRepository, IPasswordHasher passwordHasher)
        {
            _mapper = mapper;
            _bankAdminRepository = bankAdminRepository;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromQuery]BankAccountDTO bankAccountDTO)
        {
            var _BankAccount = _mapper.Map<BankAccount>(bankAccountDTO);

            _BankAccount.BankAmount = 99999;
            _BankAccount.ClientId = 1;
            _BankAccount.AccountId = 1;
            _BankAccount.BankPasswordAdmin = _passwordHasher.Hash(_BankAccount.BankPasswordAdmin);

            await _bankAdminRepository.RegisterAdmin(_BankAccount);
            var BankAccountDTO = _mapper.Map<BankAccountDTO>(_BankAccount);

            var responseApi = new ApiResponses<BankAccountDTO>(BankAccountDTO);

            return Ok(responseApi);
        }
    }
}
