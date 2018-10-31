using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using System.Collections.Generic;

namespace ContentDeliveryAPI.Samples.Customization
{
    /// <summary>
    /// Custom converter for property models
    /// with a SortOrder higher than the default converter (SortOrder = 0), ensuring that the custom converter is used 
    /// For more information, go here https://world.episerver.com/documentation/developer-guides/content-delivery-api/serialization/
    /// </summary>
    [ServiceConfiguration(typeof(IPropertyModelConverter), Lifecycle = ServiceInstanceScope.Singleton)]
    public class CustomPropertyModelConverter : DefaultPropertyModelConverter
    {
        public CustomPropertyModelConverter()
        {
            ModelTypes = new List<TypeModel>
            {
                new TypeModel {
                    ModelType = typeof(LowercaseLongStringPropertyModel), ModelTypeString = nameof(LowercaseLongStringPropertyModel), PropertyType = typeof(PropertyLongString)
                },
                new TypeModel {
                    PropertyType = typeof(PropertyContentArea), ModelType = typeof(CustomContentAreaPropertyModel), ModelTypeString = typeof(CustomContentAreaPropertyModel).FullName
                }
            };

        }

        public override int SortOrder { get; } = 100;
    }
}