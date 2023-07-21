using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Interfacez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomain.Layer___Bank_Api.Services
{
    public class ClientAccountServices : IClientAccountServices
    {
        private readonly ICrudRepository<ClientAccount> _clientCrud;
        public ClientAccountServices(ICrudRepository<ClientAccount> clientCrud)
        {
            _clientCrud = clientCrud;
        }
        public async Task<PaginationLogic<ClientAccount>> GetAll(PaginationValues value)
        {
            var clientAccount =  await _clientCrud.GetAll();

            value.PageNumber = value.PageNumber == 0 ? 1 : value.PageNumber;
            value.PageSize = value.PageSize == 0 ? 2 : value.PageSize;

            var pagination = PaginationLogic<ClientAccount>.Pagination(clientAccount, value.PageNumber, value.PageSize);
            return pagination;
        }

        public async Task<ClientAccount> GetById(int id)
        {
            return await _clientCrud.GetById(id);
        }

        public async Task Insert(ClientAccount entity)
        {
            await _clientCrud.Insert(entity);
        }

        public async Task Update(ClientAccount entity)
        {
            await _clientCrud.Update(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _clientCrud.Delete(id);
        }
    }
}
