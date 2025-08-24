using AH.Application.DTOs.Filter;
using AH.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AH.Infrastructure.Helpers
{
    public static class PersonHelper
    {
        public static void AddPersonParameters(PersonFilter personFilter, SqlCommand cmd)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["FirstName"] = (personFilter.FirstName, SqlDbType.NVarChar, 20, null),
                ["MiddleName"] = (personFilter.MiddleName, SqlDbType.NVarChar, 20, null),
                ["LastName"] = (personFilter.LastName, SqlDbType.NVarChar, 20, null),
                ["Gender"] = (personFilter.Gender, SqlDbType.Char, 1, null),
                ["BirthDateFrom"] = (personFilter.BirthDateFrom, SqlDbType.Date, null, null),
                ["BirthDateTo"] = (personFilter.BirthDateTo, SqlDbType.Date, null, null),
                ["CountryID"] = (personFilter.CountryID, SqlDbType.Int, null, null),
                ["Phone"] = (personFilter.Phone, SqlDbType.NVarChar, 8, null),
                ["Email"] = (personFilter.Email, SqlDbType.NVarChar, 40, null)
            };
            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
        }

        public static Func<SqlDataReader, Task<Person>> ReadPersonAsync = async reader =>
        {
            var converter = new ConvertingHelper(reader);

            var person = new Person
            {
                FirstName = converter.ConvertValue<string>("FirstName"),
                MiddleName = converter.ConvertValue<string>("MiddleName"),
                LastName = converter.ConvertValue<string>("LastName"),
                Gender = converter.ConvertValue<char>("Gender"),
                BirthDate = converter.ConvertValue<DateTime>("BirthDate"),
                Country = new Country
                {
                    ID = converter.ConvertValue<int>("CountryID"),
                    Name = converter.ConvertValue<string>("CountryName")
                },
                Phone = converter.ConvertValue<string>("Phone"),
                User = new User
                {
                    Email = converter.ConvertValue<string>("Email")
                }
            };

            return await Task.FromResult(person);
        };
    }
}