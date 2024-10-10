using BdJobsCorporate_CreateAccountPage.AggregateRoot.Entities;
using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdJobsCorporate_CreateAccountPage.AggregateRoot
{
    public class AccountAggregateRoot
    {
        public User User { get; private set; }
        public Company Company { get; private set; }

        public AccountAggregateRoot(CheckNamesRequestDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.UserName))
            {
                User = new User
                {
                    UserName = dto.UserName.Trim().Replace("'", "`")
                };
            }

            if (!string.IsNullOrEmpty(dto.CompanyName))
            {
                Company = new Company
                {
                    Name = dto.CompanyName.Trim().Replace("'", "`")
                };
            }
        }
    }
}
