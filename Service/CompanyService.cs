﻿using Contracts;
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

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            this.repositoryManager = repositoryManager;
            this.loggerManager = loggerManager;
        }

        public IEnumerable<CompanyDTO> GetCompanies(bool trackChanges)
        {
            try
            {
                var companies = repositoryManager.Company.GetCompanies(trackChanges);

                var companiesDto = companies.Select(c => 
                new CompanyDTO(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country)))
                    .ToList();

                return companiesDto;
            }
            catch (Exception ex)
            {
                loggerManager.LogError($"Something went wrong in the { nameof(GetCompanies)} service method { ex}");
                throw;
            }
        }
    }
}
