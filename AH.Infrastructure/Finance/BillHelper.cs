using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Infrastructure.Finance
{
    public class BillHelper
    {
        public static Bill ReadBill(SqlDataReader reader)
        {
            ConvertingHelper converter = new ConvertingHelper(reader);

            return new Bill(converter.ConvertValue<int>("BillID")
                , converter.ConvertValue<int>("Amount"),
                converter.ConvertValue<int>("AmountPaid"));
        }
    }
}