﻿using HR_LeaveManagement.Application.Contracts.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string userId);
    }
}
