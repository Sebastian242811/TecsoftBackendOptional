using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Initialization.Domain.Services.Communications
{
    public class EmployeeResponse : BaseResponse<Employee>
    {
        public EmployeeResponse(Employee resource) : base(resource)
        {
        }

        public EmployeeResponse(string message) : base(message)
        {
        }
    }
}
