//using BdJobsCorporate_CreateAccountPage.AggregateRoot.Entities;
//using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BdJobsCorporate_CreateAccountPage.AggregateRoot
//{
//    public class IndustryTypeAggregateRoot
//    {
//        public OrgType IndustryType { get; private set; }

//        public IndustryTypeAggregateRoot(IndustryTypeRequestDTO dto)
//        {
//            if (dto.IndustryId.HasValue && dto.IndustryId >= -1)
//            {
//                IndustryType = new OrgType
//                {
//                    IndustryId = dto.IndustryId.Value
//                };
//            }

//            if (!string.IsNullOrEmpty(dto.OrganizationText))
//            {
//                IndustryType.OrgTypeName = dto.OrganizationText.Trim().Replace("'", "`");
//            }

//            if (dto.CorporateID.HasValue && dto.CorporateID > 0)
//            {
//                IndustryType.CorporateID = dto.CorporateID.Value;
//            }
//        }
//    }
//}
