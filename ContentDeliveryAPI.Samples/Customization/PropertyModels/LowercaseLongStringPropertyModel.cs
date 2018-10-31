using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;

namespace ContentDeliveryAPI.Samples.Customization
{
    /// <summary>
    /// custom property model forces all PropertyLongString instances to lowercase.
    /// For more information, go here https://world.episerver.com/documentation/developer-guides/content-delivery-api/serialization/
    /// </summary>
    public class LowercaseLongStringPropertyModel : PropertyModel<string, PropertyLongString>
    {
        public LowercaseLongStringPropertyModel(PropertyLongString propertyLongString) : base(propertyLongString)
        {
            if (propertyLongString != null)
            {
                Value = propertyLongString.ToString().ToLower();
            }
        }
    }
}