using EPiServer.ContentApi.Cms;
using EPiServer.ContentApi.Core.Configuration;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace ContentDeliveryAPI.Samples.Customization
{
    /// <summary>
    ///   For more information about configuration, go here https://world.episerver.com/documentation/developer-guides/content-delivery-api/configuration/
    /// </summary>
    [ModuleDependency(typeof(ContentApiCmsInitialization))]
    public class ExtendedContentApiCmsInitialization : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {

        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            // change the default values of ContentApiOptions
            context.Services.Configure<ContentApiConfiguration>(config =>
            {
                config.Default()
                    .SetMinimumRoles("WebEditors")
                    .SetRequiredRole("WebEditors")
                    .SetSiteDefinitionApiEnabled(true)
                    .SetMultiSiteFilteringEnabled(false);
            });
        }
    }
}