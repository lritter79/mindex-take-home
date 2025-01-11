using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Compensation GetByCompensationByEmployeeId(String id);
        Employee GetByIdWithDirectReports(String id);
        ReportingStructure GetReportingStructure(string id);
        Employee Add(Employee employee);
        Compensation Add(Compensation compensation);
        Employee Remove(Employee employee);
        Task SaveAsync();
    }
}