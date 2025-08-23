using AH.Application.DTOs.Filter;
using AH.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AH.Infrastructure.Helpers
{
    public static class EmployeeHelper
    {
        public static Action<SqlCommand> AddEmployeeParameters(EmployeeFilter employeeFilter)
        {
            return cmd =>
            {
                // Reuse Person parameters
                PersonHelper.AddPersonParameters(employeeFilter)(cmd);

                // Employee-specific parameters as dictionary
                var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
                {
                    ["DepartmentID"] = (employeeFilter.DepartmentId, SqlDbType.Int, null, null),
                    ["SalaryFrom"] = (employeeFilter.SalaryFrom, SqlDbType.Money, null, null),
                    ["SalaryTo"] = (employeeFilter.SalaryTo, SqlDbType.Money, null, null),
                    ["HireDateFrom"] = (employeeFilter.HireDateFrom, SqlDbType.Date, null, null),
                    ["HireDateTo"] = (employeeFilter.HireDateTo, SqlDbType.Date, null, null),
                    ["LeaveDateFrom"] = (employeeFilter.LeaveDateFrom, SqlDbType.Date, null, null),
                    ["LeaveDateTo"] = (employeeFilter.LeaveDateTo, SqlDbType.Date, null, null),
                    ["ShiftStartFrom"] = (employeeFilter.ShiftStartFrom, SqlDbType.Time, null, null),
                    ["ShiftStartTo"] = (employeeFilter.ShiftStartTo, SqlDbType.Time, null, null),
                    ["ShiftEndFrom"] = (employeeFilter.ShiftEndFrom, SqlDbType.Time, null, null),
                    ["ShiftEndTo"] = (employeeFilter.ShiftEndTo, SqlDbType.Time, null, null),
                    ["WorkingDays"] = (employeeFilter.WorkingDays, SqlDbType.Int, null, null),
                    ["CreatedAtFrom"] = (employeeFilter.CreatedAtFrom, SqlDbType.DateTime, null, null),
                    ["CreatedAtTo"] = (employeeFilter.CreatedAtTo, SqlDbType.DateTime, null, null),
                    ["CreatedByAdminID"] = (employeeFilter.CreatedByAdminID, SqlDbType.Int, null, null)
                };

                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            };
        }

        public static Func<SqlDataReader, Task<Employee>> ReadEmployeeAsync = async reader =>
        {
            var converter = new ConvertingHelper(reader);

            var employee = new Employee
            {
                Person = await PersonHelper.ReadPersonAsync(reader),

                Department = new Department
                {
                    ID = converter.ConvertValue<int>("DepartmentID"),
                    Name = converter.ConvertValue<string>("DepartmentName")
                },
                Salary = converter.ConvertValue<int>("Salary"),
                HireDate = converter.ConvertValue<DateTime>("HireDate"),
                LeaveDate = converter.ConvertValue<DateTime>("LeaveDate"),
                WorkingDays = converter.ConvertValue<int>("WorkingDays"),
                ShiftStart = converter.ConvertValue<TimeOnly>("ShiftStart"),
                ShiftEnd = converter.ConvertValue<TimeOnly>("ShiftEnd"),
                CreatedByAdmin =
                {
                ID = converter.ConvertValue<int>("CreatedByAdminID"),

                Employee = {
                        Person =
                        {
                        FirstName= converter.ConvertValue<string>("CreatedByAdminFullName"),
                        }
                    }
                },
                CreatedAt = converter.ConvertValue<DateTime>("CreatedAt")
            };

            return await Task.FromResult(employee);
        };
    }
}