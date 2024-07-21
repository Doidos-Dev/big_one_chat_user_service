using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Test.Application.DTOs.Output
{
    public record UserOutputDTO
        (
        Guid Id,
        string Name,
        string Nickname,
        string Status,
        string Photo,
        string Email,
        string Password,
        DateTime? LastSeen,
        DateTime CreatedDate
        );
   
}
