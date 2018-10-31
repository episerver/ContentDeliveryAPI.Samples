using EPiServer.ContentApi.Core.Security;
using EPiServer.ServiceLocation;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace ContentDeliveryAPI.Samples.Customization
{
    /// <summary>
    /// For more information on how to customize authorization service, go here https://world.episerver.com/documentation/developer-guides/content-delivery-api/authorization-service/
    /// </summary>
    [ServiceConfiguration(typeof(ISecurityPrincipal))]
    public class CustomPrincipalService : ISecurityPrincipal
    {
        /// <summary>
        /// Initialize principal
        /// </summary>
        /// <param name="actionContext"></param>
        public void InitializePrincipal(HttpActionContext actionContext)
        {
            string[] roles = new string[] { "WebAdmins" };
            var principal = new GenericPrincipal(new GenericIdentity("admin"), roles);

            // call other API to verify access token 
            HttpContext.Current.User = principal;
            Thread.CurrentPrincipal = principal;

            // A real-life scenario might be a customized OAuth system where the user’s access token is retrieved by calling one service,
            // the arguments for service are retreived from htttp request
        }

        /// <summary>
        /// Get the current principal 
        /// </summary>
        /// <returns></returns>
        public IPrincipal GetCurrentPrincipal()
        {
            return HttpContext.Current?.User ?? Thread.CurrentPrincipal;
        }

        /// <summary>
        /// Retrieve an anonymous Principal with Anonymous and Everyone roles
        /// When we need to exclude personalized data, content api will filters content using this principal
        /// </summary>
        public IPrincipal GetAnonymousPrincipal()
        {
            return new GenericPrincipal(
               new GenericIdentity("Anonymous"),
               new[] { "Everyone" }
           );
        }
    }
}