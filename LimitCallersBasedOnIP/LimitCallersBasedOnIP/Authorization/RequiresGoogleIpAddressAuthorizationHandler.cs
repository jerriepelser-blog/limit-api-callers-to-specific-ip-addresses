using Microsoft.AspNetCore.Authorization;

namespace LimitCallersBasedOnIP.Authorization;

public class RequiresGoogleIpAddressAuthorizationHandler(IHttpContextAccessor httpContextAccessor, GoogleIpNetworkStorage googleIpNetworkStorage)
    : AuthorizationHandler<RequiresGoogleIpAddressRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequiresGoogleIpAddressRequirement requirement)
    {
        var remoteIpAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress;
        if (remoteIpAddress != null && googleIpNetworkStorage.ContainsIp(remoteIpAddress))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}