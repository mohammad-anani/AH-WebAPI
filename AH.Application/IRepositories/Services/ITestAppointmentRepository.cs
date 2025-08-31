using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface ITestAppointmentRepository : IService
    {
        Task<GetAllResponseDTO<TestAppointmentRowDTO>> GetAllAsync(TestAppointmentFilterDTO filterDTO);

        Task<GetAllResponseDTO<TestAppointmentRowDTO>> GetAllByPatientIDAsync(TestAppointmentFilterDTO filterDTO);

        Task<GetByIDResponseDTO<TestAppointmentDTO>> GetByIDAsync(int id);

        Task<CreateResponseDTO> AddAsync(TestAppointment testAppointment);

        Task<GetAllResponseDTO<PaymentRowDTO>> GetPaymentsAsync(ServicePaymentsDTO filterDTO);

        Task<CreateResponseDTO> AddFromTestOrderAsync(CreateTestAppointmentFromTestOrderDTO app);

        Task<SuccessResponseDTO> UpdateAsync(TestAppointment testAppointment);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}