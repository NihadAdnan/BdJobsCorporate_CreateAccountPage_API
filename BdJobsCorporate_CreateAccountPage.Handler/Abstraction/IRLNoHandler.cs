using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdJobsCorporate_CreateAccountPage.Handler.Abstraction
{
    public interface IRLNoHandler
    {
        Task<RLNoResponseDTO> Handle(RLNoRequestDTO dto);
    }
}
