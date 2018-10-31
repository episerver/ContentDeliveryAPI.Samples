using EPiServer.Security;
using EPiServer.Security.Internal;
using EPiServer.ServiceLocation;
using System.Collections.Generic;
using System.Linq;
using EPiServer.ContentApi.Core.Internal;

namespace ContentDeliveryAPI.Samples.Customization
{
    /// <summary>
    /// Responsible for manipulating with role.
    /// For more information, go here https://world.episerver.com/documentation/developer-guides/content-delivery-api/content-delivery-api-and-azure-ad/
    /// </summary>
    [ServiceConfiguration(typeof(RoleService))]
    public class CustomRoleService : RoleService
    {
        public CustomRoleService(IVirtualRoleRepository virtualRoleRepository) : base(virtualRoleRepository)
        {
        }

        ///// <summary>
        ///// Get all roles from system.
        ///// </summary>
        //public override IEnumerable<string> GetAllRoles()
        //{
        //    var synUserRepo = ServiceLocator.Current.GetInstance<ISynchronizedUsersRepository>();
        //    var rolesStatus = synUserRepo.ListRoleStatus();
        //    return (rolesStatus != null && rolesStatus.Any()) ? rolesStatus.Select(x => x.Name) : null;
        //}

        ///// <summary>
        ///// Check whether a given role is still valid.
        ///// </summary>
        //public override bool IsRoleValid(string role)
        //{
        //    var allRoles = GetAllRoles();
        //    var isValid = (allRoles != null && allRoles.Any(roleName => string.Equals(roleName, role, System.StringComparison.OrdinalIgnoreCase))) ? true : false;
        //    return isValid || base.IsRoleValid(role);
        //}
    }
}