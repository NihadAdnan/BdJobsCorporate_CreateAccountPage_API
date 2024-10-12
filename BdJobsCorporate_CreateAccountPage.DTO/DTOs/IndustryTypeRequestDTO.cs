using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdJobsCorporate_CreateAccountPage.DTO.DTOs
{
    public class IndustryTypeRequestDTO
    {
        public int? IndustryId { get; set; }  // Nullable, because it can be optional
        public string OrganizationText { get; set; }  // Optional string
        public int? CorporateID { get; set; }  // Nullable, because it can be optional
    }
}
