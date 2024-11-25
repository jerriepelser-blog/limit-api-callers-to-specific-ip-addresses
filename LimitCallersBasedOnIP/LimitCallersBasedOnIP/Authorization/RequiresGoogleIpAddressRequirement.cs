using Microsoft.AspNetCore.Authorization;

namespace LimitCallersBasedOnIP.Authorization;

public class RequiresGoogleIpAddressRequirement: IAuthorizationRequirement
{
}