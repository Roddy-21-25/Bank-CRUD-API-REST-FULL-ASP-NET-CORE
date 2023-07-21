using AplicationDomain.Layer___Bank_Api.DTOs;
using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Exceptions;
using AplicationDomain.Layer___Bank_Api.Interfacez;
using AplicationDomain.Layer___Bank_Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Layer___Bank_Api.Responses;
using System.Net;

namespace Presentation.Layer___Bank_Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAccountCRUDController : ControllerBase
    {
        private readonly IClientAccountServices _clientAccountCrud;
        private readonly IUriClientServices _uriClientServices;
        private readonly IMapper _mapper;
        public ClientAccountCRUDController(IClientAccountServices clientAccountCrud, IMapper mapper, IUriClientServices uriClientServices)
        {
            _clientAccountCrud = clientAccountCrud;
            _mapper = mapper;
            _uriClientServices = uriClientServices;
        }
        /// <summary>
        /// This will give you all the Clients Account of the API, This Api have Pagination
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponses<IEnumerable<ClientAccountDTO>>))]
        public async Task<IActionResult> GetAll([FromQuery]PaginationValues value)
        {
            var clients = await _clientAccountCrud.GetAll(value);
            var clientsAccountsDTO = _mapper.Map<IEnumerable<ClientAccountDTO>>(clients);

            var metadata = new Metadata
            {
                TotalCount = clients.TotalCount,
                PageSize = clients.PageSize,
                CurrentPage = clients.CurrentPage,
                TotalPages = clients.TotalPages,
                HasNextPage = clients.HasNextPage,
                HasPreviousPages = clients.HasPreviousPages,
                NextPageUrl = _uriClientServices.GetClientPaginationUri(value, Url.RouteUrl(nameof(GetAll))).ToString(),
                PreviousPageUrl = _uriClientServices.GetClientPaginationUri(value, Url.RouteUrl(nameof(GetAll))).ToString()
            };

            var ResponsesApi = new ApiResponses<IEnumerable<ClientAccountDTO>>(clientsAccountsDTO)
            {
                metadata = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(ResponsesApi);
        }
        /// <summary>
        /// This one doesnt have pagination, but you cant get the client Account by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="GlobalBusinessExceptions"></exception>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponses<ClientAccountDTO>))]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                throw new GlobalBusinessExceptions("The Client Account havent Found");
            }
            var client = await _clientAccountCrud.GetById(id);
            if (client == null)
            {
                throw new GlobalBusinessExceptions("The Client Account Doesnt exist, try to Post it, using the Post Method");
            }
            var clientAccountDTO = _mapper.Map<ClientAccountDTO>(client);

            var Responses = new ApiResponses<ClientAccountDTO>(clientAccountDTO);

            return Ok(Responses);
        }
        /// <summary>
        /// post a NEW Client Account, {If you dont complete any box, in SQL will be NULL.}
        /// </summary>
        /// <param name="clientDTO"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="GlobalBusinessExceptions"></exception>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponses<ClientAccountDTO>))]
        public async Task<IActionResult> Insert(
            ClientAccountDTO clientDTO, int _ClientIdAccount, string _AccountCardType)
        {
            var client = _mapper.Map<ClientAccount>(clientDTO);

            if(_ClientIdAccount == 0)
            {
                throw new GlobalBusinessExceptions("The Client doesnt exist");
            }

            client.ClientIdAccount = _ClientIdAccount;
            client.AccountCardType = _AccountCardType;

            var Responses = new ApiResponses<ClientAccountDTO>(clientDTO);

            await _clientAccountCrud.Insert(client);
            return Ok(Responses);
        }
        /// <summary>
        /// Update the Client Account {If you dont complete any box, in SQL will be NULL.}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientDTO"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="GlobalBusinessExceptions"></exception>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponses<ClientAccountDTO>))]
        public async Task<IActionResult> Update(
            int id, ClientAccountDTO clientDTO, int _ClientIdAccount, string _AccountCardType)
        {
            var client = _mapper.Map<ClientAccount>(clientDTO);

            if (_ClientIdAccount == 0)
            {
                throw new GlobalBusinessExceptions("The Client doesnt exist");
            }else if (id  == 0)
            {
                throw new GlobalBusinessExceptions("The Client doesnt exist");
            }

            client.Id = id;
            client.ClientIdAccount = _ClientIdAccount;
            client.AccountCardType = _AccountCardType;
            await _clientAccountCrud.Update(client);

            var Responses = new ApiResponses<ClientAccountDTO>(clientDTO);

            return Ok(clientDTO);
        }
        /// <summary>
        /// This will delete the client Account, By the Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="GlobalBusinessExceptions"></exception>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponses<bool>))]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _clientAccountCrud.Delete(id);
            
            var Responses = new ApiResponses<bool>(delete);

            return Ok(Responses);
        }
    }
}
