namespace AH.Application.IServices
{
    /// <summary>
    /// Base service interface for service operations like appointments and test appointments.
    /// Provides common operations for managing service statuses and scheduling.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Starts a service (appointment/test appointment) by updating its status to started.
        /// </summary>
        /// <param name="id">The unique identifier of the service to start</param>
        /// <param name="notes">Optional notes for the start operation</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        Task<ServiceResult<bool>> StartAsync(int id, string? notes);

        /// <summary>
        /// Cancels a service (appointment/test appointment) by updating its status to cancelled.
        /// </summary>
        /// <param name="id">The unique identifier of the service to cancel</param>
        /// <param name="notes">Optional notes explaining the cancellation reason</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        Task<ServiceResult<bool>> CancelAsync(int id, string? notes);

        /// <summary>
        /// Completes a service (appointment/test appointment) by updating its status to completed.
        /// </summary>
        /// <param name="id">The unique identifier of the service to complete</param>
        /// <param name="notes">Optional notes for the completion</param>
        /// <param name="result">The result or outcome of the service</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        Task<ServiceResult<bool>> CompleteAsync(int id, string? notes, string result);

        /// <summary>
        /// Reschedules a service (appointment/test appointment) to a new date and time.
        /// </summary>
        /// <param name="id">The unique identifier of the service to reschedule</param>
        /// <param name="notes">Optional notes explaining the reschedule reason</param>
        /// <param name="newScheduledDate">The new scheduled date and time</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        Task<ServiceResult<bool>> RescheduleAsync(int id, string? notes, DateTime newScheduledDate);
    }
}