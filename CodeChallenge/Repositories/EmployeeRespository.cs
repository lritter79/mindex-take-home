using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetByIdWithDirectReports(string id)
        {
            return _employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
        }

        public ReportingStructure GetReportingStructure(string id)
        {
            ReportingStructure structure = new ReportingStructure();

            if (!String.IsNullOrEmpty(id))
            {
                structure.Employee = GetByIdWithDirectReports(id);
                Func<Employee, int> getDirectReportCount = null;
                getDirectReportCount = delegate (Employee e)
                {
                    int count = 0;
                    if (e.DirectReports.Count == 0)
                    {
                        return count;
                    }
                    else
                    {

                        foreach (Employee directReport in e.DirectReports)
                        {
                            count++;
                            Employee employee = GetByIdWithDirectReports(directReport.EmployeeId);
                            count += getDirectReportCount(employee);
                        }
                    }
                    return count;
                };

                structure.NumberOfReports = getDirectReportCount(structure.Employee);
                return structure;
            }

            return null;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }

        public Compensation GetByCompensationByEmployeeId(string id)
        {
            return _employeeContext.Compensations.Include(c => c.Employee).SingleOrDefault(e => e.Employee.EmployeeId == id);
        }

        public Compensation Add(Compensation compensation)
        {
            var existingCompensation = _employeeContext.Compensations
                .SingleOrDefault(c => c.Employee.EmployeeId == compensation.Employee.EmployeeId);

            if (existingCompensation == null)
            {
                _employeeContext.Compensations.Add(compensation);
            }
            else
            {
                compensation.CompensationId = existingCompensation.CompensationId;
                _employeeContext.Entry(existingCompensation).CurrentValues.SetValues(compensation);
            }
            return compensation;
        }
    }
}
