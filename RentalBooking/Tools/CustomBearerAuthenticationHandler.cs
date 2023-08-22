using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace RentalBooking.Tools
{
    public class CustomBearerAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public CustomBearerAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Get the Authorization header
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.NoResult();
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            // Validate the token (replace with your logic)
            if (token != "xxxxxxxxxxxxxxxx") // Replace with your validation logic
            {
                return AuthenticateResult.Fail("Invalid token");
            }

            // Create a claim identity (you can add more claims based on your requirements)
            var identity = new ClaimsIdentity(Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
