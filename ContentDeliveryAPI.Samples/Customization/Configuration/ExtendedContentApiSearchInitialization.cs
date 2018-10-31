//using EPiServer.ContentApi.Search;
//using EPiServer.Framework;
//using EPiServer.Framework.Initialization;
//using EPiServer.ServiceLocation;
//using System;

//namespace ContentDeliveryAPI.Samples.Customization
//{

///// <summary>
///// If use're using EPiServer.ContentDeliveryApi.Search and want to change the default configuration.
///   For more information about configuration, go here https://world.episerver.com/documentation/developer-guides/content-delivery-api/configuration/
///// </summary>
//    [ModuleDependency(typeof(ContentApiSearchInitialization))]
//    public class ExtendedContentApiSearchInitialization : IConfigurableModule
//    {
//        public void Initialize(InitializationEngine context)
//        {
//        }

//        public void Uninitialize(InitializationEngine context)
//        {
//        }

//        public void ConfigureContainer(ServiceConfigurationContext context)
//        {

////// change the default values of ContentSearchApiOptions: 
//            context.Services.Configure<ContentApiSearchConfiguration>(config =>
//            {
//                config.Default()
//                    .SetMaximumSearchResults(200)
//                    .SetSearchCacheDuration(TimeSpan.FromMinutes(60));
//            });
//        }
//    }
//}