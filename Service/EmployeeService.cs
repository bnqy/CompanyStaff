using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly ILoggerManager loggerManager;
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public EmployeeService(IRepositoryManager repositoryManager, 
            ILoggerManager loggerManager,
            IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.loggerManager = loggerManager;
            this.mapper = mapper;
        }

        public EmployeeDTO GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            var company = repositoryManager.Company.GetCompany(companyId, trackChanges);

            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employee = repositoryManager.Employee.GetEmployee(companyId, id, trackChanges);

            if (employee is null)
            {
                throw new EmployeeNotFoundException(id);
            }

            var employeeDto = mapper.Map<EmployeeDTO>(employee);

            return employeeDto;
        }

        public IEnumerable<EmployeeDTO> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = repositoryManager.Company.GetCompany(companyId, trackChanges);

            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employees = repositoryManager.Employee.GetEmployees(companyId, trackChanges);
            var employeesDto = mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return employeesDto;
        }
    }
}
