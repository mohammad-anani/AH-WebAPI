using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Xunit.Abstractions;
using FluentAssertions;

namespace AH.Tests
{
    public class SQLParameterHelperTests
    {
        private readonly ITestOutputHelper _logger;

        public SQLParameterHelperTests(ITestOutputHelper logger)
        {
            _logger = logger;
        }

        [Fact]
        public void AddParameters_NameWithoutAt_PrefixAdded_And_DefaultDirectionInput()
        {
            using var cmd = new SqlCommand();

            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "Name", (15, SqlDbType.Int, null, null) }
            };

            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

            cmd.Parameters.Count.Should().Be(1);
            var p = cmd.Parameters[0];
            _logger.WriteLine($"Parameter name: {p.ParameterName}");
            p.ParameterName.Should().Be("@Name"); // 1
            p.Direction.Should().Be(ParameterDirection.Input); // 2
            p.Value.Should().Be(15);
        }

        [Fact]
        public void AddParameters_NameWithAt_NullValue_BecomesDBNull()
        {
            using var cmd = new SqlCommand();

            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "@Name", (null, SqlDbType.Int, null, null) }
            };

            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

            cmd.Parameters.Count.Should().Be(1);
            var p = cmd.Parameters[0];
            p.ParameterName.Should().Be("@Name"); // 3
            p.Value.Should().Be(DBNull.Value); // 4
        }

        [Fact]
        public void AddParameters_NVarCharWithoutSize_Throws()
        {
            using var cmd = new SqlCommand();

            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "Name", ("12345", SqlDbType.NVarChar, null, ParameterDirection.Output) }
            };

            Action act = () => SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            act.Should().Throw<ArgumentException>(); // 5
        }

        [Fact]
        public void AddParameters_OutputDirection_IsPreserved_WhenProvided()
        {
            using var cmd = new SqlCommand();

            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "Name", ("12345", SqlDbType.NVarChar, 10, ParameterDirection.Output) }
            };

            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

            var p = cmd.Parameters[0];
            p.Direction.Should().Be(ParameterDirection.Output); // 6
        }

        [Fact]
        public void AddParameters_ValueTypeMismatch_Throws()
        {
            using var cmd = new SqlCommand();

            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "Age", ("abc", SqlDbType.Int, null, null) }
            };

            Action act = () => SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            act.Should().Throw<ArgumentException>(); // 7
        }

        [Fact]
        public void AddParameters_StringExceedsDeclaredSize_Throws()
        {
            using var cmd = new SqlCommand();

            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "Name", ("abcdef", SqlDbType.NVarChar, 5, null) }
            };

            Action act = () => SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            act.Should().Throw<ArgumentException>(); // 8
        }

        [Fact]
        public void AddParameters_DuplicateParamNames_Rejected_EvenWithAtVariation()
        {
            using var cmd = new SqlCommand();

            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "Name", (1, SqlDbType.Int, null, null) },
                { "@Name", (2, SqlDbType.Int, null, null) }
            };

            Action act = () => SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            act.Should().Throw<ArgumentException>(); // 9
        }
    }
}