using System;
using System.Collections.Generic;


namespace UserService.Test.Application.DTOs.Input
{
    public record UserCreateDTO
        (
         string Name,
         string NickName,
         string Photo,
         string Email,
         string Password
        );
    
}
