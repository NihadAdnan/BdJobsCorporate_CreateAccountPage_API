using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdJobsCorporate_CreateAccountPage.AggregateRoot.Entities
{
    public class IndustryWiseCompany
    {
        [Key]
        public int CorporateID { get; set; }
        public int OrgTypeId { get; set; }
    }
}
