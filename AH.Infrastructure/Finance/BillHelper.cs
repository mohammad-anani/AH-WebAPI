using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;

namespace AH.Infrastructure.Finance
{
    public class BillHelper
    {
        public static Bill ReadBill(SqlDataReader reader)
        {
            ConvertingHelper converter = new ConvertingHelper(reader);

            return new Bill(converter.ConvertValue<int>("BillID")
                , converter.ConvertValue<int>("BillAmount"),
                converter.ConvertValue<int>("BillAmountPaid"));
        }
    }
}