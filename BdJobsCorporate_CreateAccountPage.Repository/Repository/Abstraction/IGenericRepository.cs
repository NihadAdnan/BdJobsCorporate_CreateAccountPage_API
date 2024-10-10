using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdJobsCorporate_CreateAccountPage.Repository.Repository.Abstraction
{
    public interface IGenericRepository
    {
        Task<bool> IsUserNameExistAsync(string userName);
        Task<bool> IsCompanyExistAsync(string companyName);
    }
}
