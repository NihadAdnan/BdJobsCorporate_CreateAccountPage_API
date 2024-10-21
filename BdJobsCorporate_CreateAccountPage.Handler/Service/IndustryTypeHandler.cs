using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
using BdJobsCorporate_CreateAccountPage.Handler.Abstraction;
using BdJobsCorporate_CreateAccountPage.Repository.Repository.Abstraction;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BdJobsCorporate_CreateAccountPage.AggregateRoot.Entities;

namespace BdJobsCorporate_CreateAccountPage.Handler.Service
{
    public class IndustryTypeHandler : IIndustryTypeHandler
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IValidator<IndustryTypeRequestDTO> _validator;

        public IndustryTypeHandler(IGenericRepository genericRepository, IValidator<IndustryTypeRequestDTO> validator)
        {
            _genericRepository = genericRepository;
            _validator = validator;
        }

        public async Task<List<IndustryTypeResponseDTO>> HandleIndustryTypeAsync(IndustryTypeRequestDTO request)
        {
            // Validate the request DTO
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                // Log validation errors here if needed
                throw new ValidationException(validationResult.Errors);
            }

            // Call repository method to fetch the data
            var industryTypes = await _genericRepository.GetIndustryTypesAsync(request.IndustryId, request.OrganizationText, request.CorporateID);

            // Handle the case where no industry types are returned
            if (industryTypes == null || !industryTypes.Any())
            {
                // Optionally log the event here
                return new List<IndustryTypeResponseDTO> { new IndustryTypeResponseDTO { IndustryValue = -1, IndustryName = "No industry types found" } };
            }

            return industryTypes;
        }

        public async Task<IEnumerable<IndustryType>> GetAllIndustrieIdsAsync()
        {
            // Call the repository method to get all industry IDs
            var industryIds = await _genericRepository.GetAllIndustrieIdsAsync();
            return industryIds;
        }
    }
}
