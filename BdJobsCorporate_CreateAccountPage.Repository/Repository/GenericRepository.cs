using BdJobsCorporate_CreateAccountPage.AggregateRoot.Entities;
using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
using BdJobsCorporate_CreateAccountPage.Repository.Data;
using BdJobsCorporate_CreateAccountPage.Repository.Repository.Abstraction;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
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

        public async Task<IEnumerable<IndustryType>> GetAllIndustrieIdsAsync()
        {
            using (var dbConnection = _context.CreateConnection())
            {
               
                dbConnection.Open(); 

                string query = "SELECT IndustryId, IndustryName FROM IndustryTypes WHERE IndustryId > 0 ORDER BY IndustryName";

                var industries = await dbConnection.QueryAsync<IndustryType>(query);
                return industries;
            }
        }



        public async Task<List<IndustryTypeResponseDTO>> GetIndustryTypesAsync(int? industryId, string organizationText = null, int? corporateID = null)
        {
            var lIndustryType = new List<IndustryTypeResponseDTO>();

            try
            {
                // Base SQL query
                var sql = @"SELECT Org_Type_ID AS IndustryValue, Org_Type_Name AS IndustryName 
                        FROM bdjDataset..ORG_TYPES 
                        WHERE (UserDefined = 0 OR (UserDefined > 0 AND VerifiedOn IS NOT NULL))";

                // Parameters for the query
                var parameters = new DynamicParameters();

                // Modify query based on input parameters
                if (industryId.HasValue && industryId.Value > 0)
                {
                    sql += " AND IndustryId = @IndustryId";
                    parameters.Add("IndustryId", industryId.Value, DbType.Int32);
                }

                if (!string.IsNullOrEmpty(organizationText))
                {
                    sql += " AND Org_Type_Name LIKE @OrganizationText";
                    parameters.Add("OrganizationText", $"%{organizationText}%", DbType.String);
                }

                sql += " ORDER BY Org_Type_Name";

                // Using DapperDbContext to create a connection
                using (var dbConnection = _context.CreateConnection())
                {
                    // Ensure the connection is open
                    dbConnection.Open();

                    // Execute the query and map results to IndustryTypeResponseDTO
                    var results = await dbConnection.QueryAsync<IndustryTypeResponseDTO>(sql, parameters);
                    lIndustryType = results.ToList();

                    // If no data returned, handle it with a default response
                    if (!lIndustryType.Any())
                    {
                        lIndustryType.Add(new IndustryTypeResponseDTO { IndustryValue = -1, IndustryName = "null" });
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally, add logging here
                throw new Exception("Error while fetching Industry Types", ex);
            }

            return lIndustryType;
        }


        public async Task<RLNoDataDTO> GetRLNoDataAsync(string rlNo)
        {
            const string sql = @"
        SELECT raStatus AS RL_no, NAME AS Company_Name 
        FROM adm.RecruitingAgencies 
        WHERE RLNO = @RlNo";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                // Open the connection synchronously
                dbConnection.Open();

                // Execute the query asynchronously
                var result = await dbConnection.QuerySingleOrDefaultAsync<RLNoDataDTO>(sql, new { RlNo = rlNo });

                return result;
            }
        }


    }
}
