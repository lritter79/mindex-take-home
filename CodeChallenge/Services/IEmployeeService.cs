using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    public interface IEmployeeService
    {
        Employee GetById(String id);
        Employee Create(Employee employee);
        Compensation CreateCompensation(Compensation compensation);
        Compensation GetCompensationByEmployeeId(String id);
        ReportingStructure GetReportingStructure(String id);
        Employee Replace(Employee originalEmployee, Employee newEmployee);
    }
}
