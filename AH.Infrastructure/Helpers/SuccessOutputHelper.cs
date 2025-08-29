using Microsoft.Data.SqlClient;

namespace AH.Infrastructure.Helpers
{
    public class SuccessOutputHelper
    {
        private SqlParameter outputParam;

        public SuccessOutputHelper()
        {
            outputParam = new SqlParameter
            {
                ParameterName = "@Success",
                SqlDbType = System.Data.SqlDbType.Bit,
                Direction = System.Data.ParameterDirection.Output
            };
        }

        public void AddToCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(outputParam);
        }

        public bool GetResult()
        {
            if (outputParam.Value == DBNull.Value || outputParam.Value == null)
                return false;
            return (bool)outputParam.Value;
        }
    }
}