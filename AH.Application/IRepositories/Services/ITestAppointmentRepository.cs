using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IRepositories
{
    public interface ITestAppointmentRepository : IService
    {
        Task<GetAllResponseDTO<TestAppointmentRowDTO>> GetAllAsync(TestAppointmentFilterDTO filterDTO);

        Task<GetAllResponseDTO<TestAppointmentRowDTO>> GetAllByPatientIDAsync(TestAppointmentFilterDTO filterDTO);

        Task<GetByIDResponseDTO<TestAppointmentDTO>> GetByIDAsync(int id);

        Task<int> AddAsync(TestAppointment testAppointment);

        Task<int> AddFromTestOrderAsync(TestAppointment testAppointment);

        Task<bool> UpdateAsync(TestAppointment testAppointment);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}