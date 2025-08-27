using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Infrastructure.Helpers
{
    public class IdOutputHelper
    {
        private SqlParameter outputParam;

        public IdOutputHelper()
        {
            outputParam = new SqlParameter
            {
                ParameterName = "@ID",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };
        }

        public void AddToCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(outputParam);
        }

        public int GetNewID()
        {
            if (outputParam.Value == DBNull.Value || outputParam.Value == null)
                return -1;
            return (int)outputParam.Value;
        }
    }
}