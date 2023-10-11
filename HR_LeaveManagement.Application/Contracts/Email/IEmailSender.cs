using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
