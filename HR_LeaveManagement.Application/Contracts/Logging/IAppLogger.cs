using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Logging
{
    public interface IAppLogger<T>
    {
        public void LogInformation(string message, params object[] args);
        public void LogWarning(string message, params object[] args);
    }
}
