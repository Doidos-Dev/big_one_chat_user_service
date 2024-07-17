using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Test.Application.DTOs.Input
{
    public record UserUpdateDTO
        (
         Guid Id,
         string Name,
         string NickName,
         string Photo,
         string Email,
         string Password
        );
   
}
