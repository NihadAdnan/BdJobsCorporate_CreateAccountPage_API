using BdJobsCorporate_CreateAccountPage.Repository.Data;
using BdJobsCorporate_CreateAccountPage.Repository.Repository.Abstraction;
using Dapper;
using System.Threading.Tasks;

namespace BdJobsCorporate_CreateAccountPage.Repository.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DapperDbContext _context;

        public GenericRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            string query = "SELECT COUNT(1) FROM CorporateUserAccess WHERE User_Name = @UserName";

            using (var connection = _context.CreateConnection())
            {
                var count = await connection.ExecuteScalarAsync<int>(query, new { UserName = userName });
                return count > 0;
            }
        }

        public async Task<bool> IsCompanyExistAsync(string companyName)
        {
            var normalizedCompanyName = NormalizeCompanyName(companyName);

            string query = @"
                SELECT COUNT(1) 
                FROM Dbo_Company_Profiles 
                WHERE (OfflineCom IS NULL OR OfflineCom = 0) 
                AND LOWER(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Name, ' Limited',''), ' Ltd.',''), ' Ltd',''), ' Private',''), ' Pvt.',''), '(Pvt.)',''), ' Pvt',''), '(Pvt)',''), ' company',''), ' Co.',''), '(Co.)',''), '.',''), ' ',''), '(',''), ')',''))
                = @NormalizedCompanyName";

            using (var connection = _context.CreateConnection())
            {
                var count = await connection.ExecuteScalarAsync<int>(query, new { NormalizedCompanyName = normalizedCompanyName });
                return count > 0;
            }
        }

        private string NormalizeCompanyName(string companyName)
        {
            return companyName
                .Replace(" Limited", "")
                .Replace(" Ltd.", "")
                .Replace(" Ltd", "")
                .Replace(" Private", "")
                .Replace(" Pvt.", "")
                .Replace("(Pvt.)", "")
                .Replace(" Pvt", "")
                .Replace("(Pvt)", "")
                .Replace(" company", "")
                .Replace(" Co.", "")
                .Replace("(Co.)", "")
                .Replace(".", "")
                .Replace(" ", "")
                .Replace("(", "")
                .Replace(")", "")
                .ToLower();
        }
    }
}
