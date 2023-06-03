using Microsoft.AspNetCore.RateLimiting;

namespace SampleAPI.Configuration;

public static class RateLimitingConfiguration
{
    public static IServiceCollection ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(_ => _
            .AddFixedWindowLimiter(policyName: "fixed", options =>
            {
                options.PermitLimit = 10;
                options.Window = TimeSpan.FromSeconds(1);
                options.QueueLimit = 0;
            }).RejectionStatusCode = 429);
        
        return services;
    }
}