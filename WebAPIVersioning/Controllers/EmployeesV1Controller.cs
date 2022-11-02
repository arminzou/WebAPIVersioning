using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIVersioning.Models;

namespace WebAPIVersioning.Controllers
{
    public class EmployeesV1Controller : ApiController
    {
        List<EmployeeV1> employees = new List<EmployeeV1>()
        {
            new EmployeeV1() { EmployeeID = 101, EmployeeName = "Anurag"},
            new EmployeeV1() { EmployeeID = 102, EmployeeName = "Priyanka"},
            new EmployeeV1() { EmployeeID = 103, EmployeeName = "Sambit"},
            new EmployeeV1() { EmployeeID = 104, EmployeeName = "Preety"},
        };
        public IEnumerable<EmployeeV1> Get()
        {
            return employees;
        }
        public EmployeeV1 Get(int id)
        {
            return employees.FirstOrDefault(s => s.EmployeeID == id);
        }
    }
}
