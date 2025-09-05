using AH.Application.DTOs.Response;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AH.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ILogger<CountryRepository> _logger;

        public CountryRepository(ILogger<CountryRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<Country>> GetAllAsync()
        {
            List<Country> items = new();
            ConvertingHelper converter = new ConvertingHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                "Fetch_Countries",
                _logger,
                null,
                (reader, cmd) =>
                {
                    items.Add(new Country(
                        converter.ConvertValue<int>("ID"),
                        converter.ConvertValue<string>("Name")
                    ));
                },
                null,
                (reader, cmd) => { converter = new ConvertingHelper(reader); }
            );

            return new GetAllResponseDTO<Country>(items, items.Count, ex);
        }
    }
}
