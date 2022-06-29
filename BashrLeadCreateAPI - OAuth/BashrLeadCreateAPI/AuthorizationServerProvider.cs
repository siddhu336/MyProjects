using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using BashrLeadCreateAPI.Helper;
using Microsoft.Owin.Security;

namespace BashrLeadCreateAPI
{
    public class AuthorizationServerProvider:OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            context.TryGetFormCredentials(out clientId, out clientSecret);
            CommonHelpers _chelper = new CommonHelpers();
            Microsoft.Xrm.Sdk.Entity userEntity = _chelper.ValidateClientIDSecretinCRM(clientId, clientSecret);

            if (userEntity != null && userEntity.Id.ToString().ToUpper() == clientSecret.ToString().ToUpper())
            {
                context.Validated(clientId);
            }

            return base.ValidateClientAuthentication(context);
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            CommonHelpers _chelper = new CommonHelpers();
            Microsoft.Xrm.Sdk.Entity userEntity = _chelper.ValidateUserDetailsInCRM(context);
            if (userEntity != null)
            {
                if (userEntity.Id.ToString().ToUpper() == context.Password.ToUpper())
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "ServiceAccount"));
                    identity.AddClaim(new Claim("username",(string)userEntity.Attributes["fullname"]));
                    identity.AddClaim(new Claim(ClaimTypes.Name, (string)userEntity.Attributes["fullname"]));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("Invalid Password", "UserName and Password Combination is incorrect. Please check and retry.");
                    return;
                }
            }
            else
            {
                context.SetError("Invalid Grant", "Provide User Name and Password");
                return;
            }
            //if (context.UserName == "admin" && context.Password == "admin")
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            //    identity.AddClaim(new Claim("username", "admin"));
            //    identity.AddClaim(new Claim(ClaimTypes.Name, "Rupesh"));
            //    context.Validated(identity);
            //}
            //else if (context.UserName == "user" && context.Password == "user")
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            //    identity.AddClaim(new Claim("username", "user"));
            //    identity.AddClaim(new Claim(ClaimTypes.Name, "Nekkanti"));
            //    context.Validated(identity);
            //}
            //else
            //{
            //    context.SetError("Invalid Grant", "Provide User Name and Password");
            //    return;
            //}
        }

       
        
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            //var client = clientService.GetClient(context.ClientId);
           
            CommonHelpers _chelper = new CommonHelpers();
            //Microsoft.Xrm.Sdk.Entity userEntity = _chelper.ValidateClientIDSecretinCRM(context.ClientId);
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "ServiceAccountRole"));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);
            return base.GrantClientCredentials(context);
        }

    }
}