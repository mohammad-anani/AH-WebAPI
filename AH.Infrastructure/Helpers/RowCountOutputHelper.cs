using Microsoft.Data.SqlClient;

namespace AH.Infrastructure.Helpers
{
    public class RowCountOutputHelper
    {
        private SqlParameter outputParam;

        public RowCountOutputHelper()
        {
            outputParam = new SqlParameter
            {
                ParameterName = "@TotalRowCount",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };
        }

        public void AddToCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(outputParam);
        }

        public int GetRowCount()
        {
            if (outputParam.Value == DBNull.Value || outputParam.Value == null)
                return -1;
            return (int)outputParam.Value;
        }
    }
}