using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext repositoryContext;
        private readonly Lazy<ICompanyRepository> companyRepository;
        private readonly Lazy<IEmployeeRepository> employeeRepository;

        public ICompanyRepository Company => companyRepository.Value;

        public IEmployeeRepository Employee => employeeRepository.Value;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
            this.companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(this.repositoryContext));
            this.employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(this.repositoryContext));
        }

        public void Save()
        {
            repositoryContext.SaveChanges();
        }
    }
}
