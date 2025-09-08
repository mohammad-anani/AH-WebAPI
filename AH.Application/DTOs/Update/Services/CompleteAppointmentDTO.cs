using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class CompleteAppointmentDTO : CompleteServiceDTO
    {
        // List of related test type IDs to be converted to comma separated string for SP parameter (TestOrders)
        [MinLength(1, ErrorMessage = "At least one test type id is required when providing test orders.")]
        public List<int> TestTypeIDs { get; set; } = new();
    }
}
