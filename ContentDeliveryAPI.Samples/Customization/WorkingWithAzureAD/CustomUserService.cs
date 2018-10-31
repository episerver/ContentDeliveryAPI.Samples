using EPiServer.ContentApi.Core.Internal;
using EPiServer.Security;
using EPiServer.Security.Internal;
using EPiServer.ServiceLocation;
using System.Linq;

namespace ContentDeliveryAPI.Samples.Customization
{
    /// <summary>
    /// Custom user service to work with Azure AD
    /// For more information, go here https://world.episerver.com/documentation/developer-guides/content-delivery-api/content-delivery-api-and-azure-ad/
    /// </summary>
    [ServiceConfiguration(typeof(UserService))]
    public class CustomUserService : UserService
    {
        public CustomUserService(IContentAccessEvaluator accessEvaluator) : base(accessEvaluator)
        {

        }

        ///// <summary>
        ///// Check whether an user is valid
        ///// </summary>
        //public override bool IsUserValid(string name)
        //{
        //    var synUserRepo = ServiceLocator.Current.GetInstance<ISynchronizedUsersRepository>();

        //    var users = synUserRepo.FindUsers(name);
        //    return users != null && users.Any();
        //}
    }
}