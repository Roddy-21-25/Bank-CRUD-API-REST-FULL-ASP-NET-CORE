using AplicationDomain.Layer___Bank_Api.DTOs;
using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Exceptions;
using AplicationDomain.Layer___Bank_Api.Interfacez;
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
    public class ClientCRUDController : ControllerBase
    {
        private readonly IClientServices _clientCrud;
        private readonly IMapper _mapper;
        private readonly IUriClientServices _uriClientServices;
        public ClientCRUDController(IClientServices clientCrud, IMapper mapper, IUriClientServices uriClientServices)
        {
            _clientCrud = clientCrud;
            _mapper = mapper;
            _uriClientServices = uriClientServices;
        }

        /// <summary>
        /// This will give you all the Clients of the API, This Api have Pagination        
        /// </summary>
        /// <param name="value">Prueba</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponses<IEnumerable<ClientDTO>>))]
        public async Task<IActionResult> GetAll([FromQuery] PaginationValues value)
        {
            var clients = await _clientCrud.GetAll(value);
            var clientsDTOs = _mapper.Map<IEnumerable<ClientDTO>>(clients);

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

            var ResponseApi = new ApiResponses<IEnumerable<ClientDTO>>(clientsDTOs)
            {
                metadata = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(ResponseApi);
        }
        /// <summary>
        /// This one doesnt have pagination, but you cant get the client by the id.
        /// </summary>
        /// <param name="id">KLK</param>
        /// <returns></returns>
        /// <exception cref="GlobalBusinessExceptions"></exception>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponses<ClientDTO>))]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                throw new GlobalBusinessExceptions("The Client havent Found");
            }

            var client = await _clientCrud.GetById(id);
            if (client == null)
            {
                throw new GlobalBusinessExceptions("The Client Doesnt exist, try to Post it, using the Post Method");
            }
            var clientDTO = _mapper.Map<ClientDTO>(client);

            var Responses = new ApiResponses<ClientDTO>(clientDTO);

            return Ok(Responses);
        }
        /// <summary>
        /// post a NEW Client {If you dont complete any box, in SQL will be NULL.}
        /// </summary>
        /// <param name="clientDTO"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="GlobalBusinessExceptions"></exception>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponses<ClientDTO>))]
        public async Task<IActionResult> Insert(ClientDTO clientDTO, string password)
        {
            var client = _mapper.Map<Client>(clientDTO);

            if(password == null)
            {
                throw new GlobalBusinessExceptions("You will need to put a password before create a user");
            }

            if(password.Length > 12)
            {
                throw new GlobalBusinessExceptions("The Password is less than 12 caracteres");
            }

            var hashPassword = _clientCrud.HashPassword(password);
            client.ClientPassword = hashPassword;

            await _clientCrud.Insert(client);

            var Responses = new ApiResponses<ClientDTO>(clientDTO);

            return Ok(Responses);
        }
        /// <summary>
        /// Update the Client {If you dont complete any box, in SQL will be NULL.}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientDTO"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="GlobalBusinessExceptions"></exception>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponses<ClientDTO>))]
        public async Task<IActionResult> Update(int id, ClientDTO clientDTO, string password)
        {
            if (id == 0)
            {
                throw new GlobalBusinessExceptions("The Client havent Found");
            }
            else if(password.Length > 12)
            {
                throw new GlobalBusinessExceptions("The Password is less than 12 caracteres");
            }

            var client = _mapper.Map<Client>(clientDTO);
            client.Id = id;

            var hashPassword = _clientCrud.HashPassword(password);
            client.ClientPassword = hashPassword;

            await _clientCrud.Update(client);

            var Responses = new ApiResponses<ClientDTO>(clientDTO);

            return Ok(Responses);
        }
        /// <summary>
        /// This will delete the client, By the Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="GlobalBusinessExceptions"></exception>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponses<bool>))]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                throw new GlobalBusinessExceptions("The Client havent Found");
            }

            var delete = await _clientCrud.Delete(id);
            
            var Response = new ApiResponses<bool>(delete);

            return Ok(Response);
        }
    }
}
