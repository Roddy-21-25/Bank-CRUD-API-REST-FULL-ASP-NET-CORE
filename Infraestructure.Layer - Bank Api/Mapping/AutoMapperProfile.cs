using AplicationDomain.Layer___Bank_Api.DTOs;
using AplicationDomain.Layer___Bank_Api.Entities;
using AutoMapper;

namespace Infraestructure.Layer___Bank_Api.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();

            CreateMap<ClientAccount, ClientAccountDTO>();
            CreateMap<ClientAccountDTO, ClientAccount>();

            CreateMap<BankAccount, BankAccountDTO>();
            CreateMap<BankAccountDTO, BankAccount>();
        }
    }
}
