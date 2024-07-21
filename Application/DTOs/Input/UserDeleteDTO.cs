using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Input
{
    public record UserDeleteDTO(
       string Nickname,
       string Password
       );
}
