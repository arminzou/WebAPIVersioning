using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIVersioning.Controllers
{
    public class EmployeesV2Controller : ApiController
    {
        List<EmployeeV2> employees = new List<EmployeeV2>()
        {
            new EmployeeV2() { EmployeeID = 101, FirstName = "Anurag", LastName = "Mohanty"},
            new EmployeeV2() { EmployeeID = 102, FirstName = "Priyanka", LastName = "Dewangan"},
            new EmployeeV2() { EmployeeID = 103, FirstName = "Sambit", LastName = "Satapathy"},
            new EmployeeV2() { EmployeeID = 104, FirstName = "Preety", LastName = "Tiwary"},
        };
        public IEnumerable<EmployeeV2> Get()
        {
            return employees;
        }
        public EmployeeV2 Get(int id)
        {
            return employees.FirstOrDefault(s => s.EmployeeID == id);
        }
    }
}
