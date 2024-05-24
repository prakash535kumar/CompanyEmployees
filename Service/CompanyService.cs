using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        ////Before Implementing Global Error Handling
        //public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        //{
        //    try
        //    {
        //        var companies = _repository.Company.GetAllCompanies(trackChanges);
        //        //List<CompanyDto> companiesDto = companies.Select(c => new CompanyDto(c.id, c.Name ?? "", string.Join(' ', c.Address, c.Country))).ToList();
        //        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        //        return companiesDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} service method {ex}");
        //        throw;
        //    }
        //}

        //After Implementing Global Error Handling
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;
        }

        public CompanyDto GetCompany(Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(id, trackChanges);
            //Check if the company is null 
            if (company == null)
                throw new CompanyNotFoundException(id);

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }
    }
}
