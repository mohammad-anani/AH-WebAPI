using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Domain.Entities;
using AH.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AH.Infrastructure.Helpers
{
    public static class EmployeeHelper
    {
        public static void AddEmployeeFilterParameters(EmployeeFilter employeeFilter, SqlCommand cmd)
        {
            // Reuse Person parameters
            PersonHelper.AddPersonFilterParameters(employeeFilter, cmd);

            int workingdays = Employee.ToBitmask((employeeFilter.WorkingDays ?? "").Split(',').ToList());

            // Employee-specific parameters as dictionary
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["DepartmentID"] = (employeeFilter.DepartmentID, SqlDbType.Int, null, null),
                ["SalaryFrom"] = (employeeFilter.SalaryFrom, SqlDbType.Int, null, null),
                ["SalaryTo"] = (employeeFilter.SalaryTo, SqlDbType.Int, null, null),
                ["HireDateFrom"] = (employeeFilter.HireDateFrom, SqlDbType.Date, null, null),
                ["HireDateTo"] = (employeeFilter.HireDateTo, SqlDbType.Date, null, null),
                ["LeaveDateFrom"] = (employeeFilter.LeaveDateFrom, SqlDbType.Date, null, null),
                ["LeaveDateTo"] = (employeeFilter.LeaveDateTo, SqlDbType.Date, null, null),
                ["ShiftStartFrom"] = (employeeFilter.ShiftStartFrom, SqlDbType.Time, null, null),
                ["ShiftStartTo"] = (employeeFilter.ShiftStartTo, SqlDbType.Time, null, null),
                ["ShiftEndFrom"] = (employeeFilter.ShiftEndFrom, SqlDbType.Time, null, null),
                ["ShiftEndTo"] = (employeeFilter.ShiftEndTo, SqlDbType.Time, null, null),
                ["WorkingDays"] = (workingdays == -1 ? null : workingdays, SqlDbType.Int, null, null),
            };

            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

            AdminAuditHelper.AddAdminAuditParameters
                (employeeFilter.CreatedByAdminID, employeeFilter.CreatedAtFrom, employeeFilter.CreatedAtTo, cmd);
        }

        public static void AddCreateEmployeeParameters(Employee employee, SqlCommand cmd)
        {
            // Reuse Person parameters
            PersonHelper.AddCreatePersonParameters(employee.Person, cmd);

            // Employee-specific parameters as dictionary
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["DepartmentID"] = (employee.Department.ID, SqlDbType.Int, null, null),
                ["Salary"] = (employee.Salary, SqlDbType.Int, null, null),
                ["HireDate"] = (employee.HireDate, SqlDbType.Date, null, null),
                ["ShiftStart"] = (employee.ShiftStart, SqlDbType.Time, null, null),
                ["ShiftEnd"] = (employee.ShiftEnd, SqlDbType.Time, null, null),
                ["WorkingDays"] = (employee.WorkingDays, SqlDbType.Int, null, null),
                ["CreatedByAdminID"] = (employee.CreatedByAdmin?.ID, SqlDbType.Int, null, null)
            };

            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
        }

        public static void AddUpdateEmployeeParameters(Employee employee, SqlCommand cmd)
        {
            // Reuse Person parameters
            PersonHelper.AddUpdatePersonParameters(employee.Person, cmd);

            // Employee-specific parameters as dictionary
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["DepartmentID"] = (employee.Department.ID, SqlDbType.Int, null, null),
                ["Salary"] = (employee.Salary, SqlDbType.Int, null, null),
                ["HireDate"] = (employee.HireDate, SqlDbType.Date, null, null),
                ["ShiftStart"] = (employee.ShiftStart, SqlDbType.Time, null, null),
                ["ShiftEnd"] = (employee.ShiftEnd, SqlDbType.Time, null, null),
                ["WorkingDays"] = (employee.WorkingDays, SqlDbType.Int, null, null),
            };

            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
        }

        public static Func<SqlDataReader, EmployeeDTO> ReadEmployee = reader =>
        {
            var converter = new ConvertingHelper(reader);

            int? createdByAdminID = converter.ConvertValue<int?>("CreatedByAdminID");
            var employee = new EmployeeDTO
            (
                 PersonHelper.ReadPerson(reader),

                 DepartmentRepository.ReadDepartment(reader),
                 converter.ConvertValue<int>("Salary"),
                converter.ConvertValue<DateOnly>("HireDate"),
                 converter.ConvertValue<DateOnly?>("LeaveDate"),
                Employee.FromBitmask(converter.ConvertValue<int>("WorkingDays")).Split(",").ToList(),
                converter.ConvertValue<TimeOnly>("ShiftStart"),
                converter.ConvertValue<TimeOnly>("ShiftEnd"),
                 createdByAdminID != null ? AdminAuditHelper.ReadAdmin(reader) : null,
converter.ConvertValue<DateTime>("CreatedAt")

                );

            return employee;
        };
    }
}