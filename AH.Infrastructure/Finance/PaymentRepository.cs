using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System.Data;
using AH.Application.DTOs.Entities;
using AH.Infrastructure.Finance;

namespace AH.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ILogger<PaymentRepository> _logger;

        public PaymentRepository(ILogger<PaymentRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<PaymentRowDTO>> GetAllByBillIDAsync(PaymentFilterDTO filterDTO)
        {
            var extraParameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["BillID"] = (filterDTO.BillID, SqlDbType.Int, null, null),
                ["Page"] = (filterDTO.Page, SqlDbType.Int, null, null),
            };

            int totalCount = -1;
            List<PaymentRowDTO> items = new List<PaymentRowDTO>();
            ConvertingHelper converter = new ConvertingHelper();
            RowCountOutputHelper rowCountOutputHelper = new RowCountOutputHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 "Fetch_Payments", _logger, cmd =>
                 {
                     SqlParameterHelper.AddParametersFromDictionary(cmd, extraParameters);

                     rowCountOutputHelper.AddToCommand(cmd);
                 }, (reader, cmd) =>
                 {
                     items.Add(new PaymentRowDTO(
                         converter.ConvertValue<int>("ID"),
                         converter.ConvertValue<int>("Amount"),
                         converter.ConvertValue<string>("Method")
                        ));
                 }, null, (reader, cmd) => { converter = new ConvertingHelper(reader); });

            totalCount = rowCountOutputHelper.GetRowCount();

            return new GetAllResponseDTO<PaymentRowDTO>(items, totalCount, ex);
        }

        public async Task<GetByIDResponseDTO<PaymentDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<PaymentDTO>("Fetch_PaymentByID", _logger, id, null, (reader, converter) =>
            new PaymentDTO(converter.ConvertValue<int>("ID"),
                BillHelper.ReadBill(reader),
                converter.ConvertValue<int>("AmountToPay"),
                converter.ConvertValue<string>("Method"),
                converter.ConvertValue<DateTime>("CreatedAt"), ReceptionistAuditHelper.ReadReceptionist(reader)));
        }

        public async Task<CreateResponseDTO> AddAsync(Payment payment)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["BillID"]= (payment.Bill?.ID, SqlDbType.Int, null, null),
                ["Amount"]= (payment.Amount, SqlDbType.Int, null, null),
                ["Method"]= (Payment.GetMethod(payment.Method), SqlDbType.Int, null, null),
                ["CreatedByReceptionistID"]= (payment.CreatedByReceptionist?.ID, SqlDbType.Int, null, null)
            };

            return await ReusableCRUD.AddAsync("Create_Payment", _logger, (cmd) =>
            {
                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

            });
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_Payment", _logger, id);
        }
    }
}