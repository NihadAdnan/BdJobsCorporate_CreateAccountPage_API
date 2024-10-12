using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdJobsCorporate_CreateAccountPage.DTO.DTOs
{
    public class IndustryTypeResponseDTO
    {
        public int IndustryValue { get; set; }  // Represents Org_Type_ID in the database
        public string IndustryName { get; set; }
    }
}
