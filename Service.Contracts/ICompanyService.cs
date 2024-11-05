using Shared.DTOs;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDTO> GetCompanies(bool trackChanges);
    }
}
