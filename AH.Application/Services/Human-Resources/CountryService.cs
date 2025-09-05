using AH.Application.DTOs.Response;
using AH.Application.IRepositories;
using AH.Application.IServices;
using AH.Domain.Entities;

namespace AH.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<ServiceResult<GetAllResponseDataDTO<Country>>> GetAllAsync()
        {
            var response = await _countryRepository.GetAllAsync();
            var data = new GetAllResponseDataDTO<Country>(response);
            return ServiceResult<GetAllResponseDataDTO<Country>>.Create(data, response.Exception);
        }
    }
}
