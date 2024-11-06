using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly ILoggerManager loggerManager;
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public CompanyService(IRepositoryManager repositoryManager, 
            ILoggerManager loggerManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.loggerManager = loggerManager;
            this.mapper = mapper;
        }

        public IEnumerable<CompanyDTO> GetCompanies(bool trackChanges)
        {
            var companies = repositoryManager.Company.GetCompanies(trackChanges);

            var companiesDto = mapper.Map<IEnumerable<CompanyDTO>>(companies);

            return companiesDto;
        }
    }
}
