using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Security.Jwt;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IJwtHelper
    {
        AccessToken CreateToken(User user, IEnumerable<OperationClaim> operationClaims);
    }
}
