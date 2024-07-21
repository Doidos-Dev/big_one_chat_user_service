using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Test.Application.DTOs.Output
{
    public record SettingsOutputDTO
        (
        Guid Id,
        Guid UserId,
        bool IsVisibleStatus,
        bool IsVisibleLastSeen,
        bool IsVisibleMessageSeen
        );
    
    
}
