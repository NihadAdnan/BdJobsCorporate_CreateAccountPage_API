using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdJobsCorporate_CreateAccountPage.AggregateRoot.Entities
{
    public class OrgType
    {
        public int OrgTypeId { get; set; }
        public string OrgTypeName { get; set; }
        public int UserDefined { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public int IndustryId { get; set; }

    }
}
