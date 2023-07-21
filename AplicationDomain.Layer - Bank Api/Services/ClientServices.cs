using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Interfacez;
using AplicationDomain.Layer___Bank_Api.Paginations_Options;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace AplicationDomain.Layer___Bank_Api.Services
{
    public class ClientServices : IClientServices
    {
        private readonly ICrudRepository<Client> _clientCrud;
        private readonly PaginationDefaultValues _paginationDefaultValues;
        public ClientServices(ICrudRepository<Client> clientCrud, IOptions<PaginationDefaultValues> paginationDefaultValues)
        {
            _clientCrud = clientCrud;
            _paginationDefaultValues = paginationDefaultValues.Value;
        }

        public async Task<PaginationLogic<Client>> GetAll(PaginationValues value)
        {
            var clients =  await _clientCrud.GetAll();

            value.PageNumber = value.PageNumber == 0 ? _paginationDefaultValues.DefaultPageNumber : value.PageNumber;
            value.PageSize = value.PageSize == 0 ? _paginationDefaultValues.DefaultPageSize : value.PageSize;

            var PaginationResponse = PaginationLogic<Client>.Pagination(clients, value.PageNumber, value.PageSize);
            return PaginationResponse;
        }

        public async Task<Client> GetById(int id)
        {
            return await _clientCrud.GetById(id);
        }

        public async Task Insert(Client entity)
        {
            await _clientCrud.Insert(entity);
        }

        public async Task Update(Client entity)
        {
            await _clientCrud.Update(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _clientCrud.Delete(id);
        }

        public string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
