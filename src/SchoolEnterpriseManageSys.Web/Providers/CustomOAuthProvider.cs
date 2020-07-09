using Abp.Dependency;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using SchoolEnterpriseManageSys.User;
using SchoolEnterpriseManageSys.User.Dto;
//using OwnerSayCar.User;
//using OwnerSayCar.User.Dto;
//using OwnerSayCar.Utilities.Http;
//using OwnerSayCar.Web.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SchoolEnterpriseManageSys.Web.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            var audience = AudiencesStore.FindAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            string wechatLoginKey = ConfigurationManager.AppSettings["wxLoginKey"];


            var identity = new ClaimsIdentity("JWT");


            #region 系统登录
            using (var userService = IocManager.Instance.ResolveAsDisposable<IUserService>())
            {
                LoginOutput output = userService.Object.Login(new LoginInput { Password = context.Password, UserName = context.UserName });

                if (!output.Id.HasValue)
                {
                    context.SetError("invalid_grant", "账户或密码错误");
                    return Task.FromResult<object>(null);
                }


                identity.AddClaim(new Claim("UserId", output.Id.ToString()));
                identity.AddClaim(new Claim("UserName", output.UserName));


                string targetName = !string.IsNullOrEmpty(output.ActualName) ? output.ActualName : output.Nickname;
                identity.AddClaim(new Claim("NickName", !string.IsNullOrEmpty(targetName) ? targetName : ""));


                identity.AddClaim(new Claim("UserType", output.UserType.ToString()));
                identity.AddClaim(new Claim("RoleId", output.RoleId.ToString()));
                identity.AddClaim(new Claim("Role", output.Role));
            }
            #endregion


            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", context.ClientId ??string.Empty                }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }

    }
}