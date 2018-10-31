using EPiServer.ContentApi.Core.ContentResult;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;

namespace ContentDeliveryAPI.Samples.Customization
{
    /// <summary>
    /// This service takes responsibility to handle data before returning it to clients. 
    /// Here, this class is customized so that it can limit the payload returned to clients. 
    /// With this customized class, only fields needed by clients can be returned through the API.
    /// </summary>
    [ServiceConfiguration(typeof(ContentResultService))]
    public class CustomContentResultService : ContentResultService
    {
        public CustomContentResultService(IContentApiSerializer contentApiSerializer) : base(contentApiSerializer)
        {

        }

        /// <summary>
        /// Build string content from object use given serializer
        /// (1) Only return needed fields to clients 
        /// (2) Only applied for content api not search api
        /// Enadpoint sample should be http://localhost:8080/api/episerver/v2.0/content/12?fields=contentLink,name,language,productPageLinks,relatedContentArea,mainContentArea
        /// where fields in query string splited by a comma. Only fields presented will be returned
        /// </summary>
        public override StringContent BuildContent(object value)
        {
            var fields = System.Web.HttpContext.Current.Request.Params["fields"];
            if (string.IsNullOrEmpty(fields) || !(value is ContentApiModel))
            {
                return base.BuildContent(value);
            }

            var returnedProperties = fields.Split(',');
            var convertedObj = new ExpandoObject() as IDictionary<string, Object>;

            Func<string[], string, bool> shouldIncludeProperty = (propertyList, property) =>
            {
                return propertyList.Any(prop => string.Equals(prop, property, StringComparison.InvariantCultureIgnoreCase));
            };

            foreach (var prop in value.GetType().GetProperties())
            {
                var propertyType = prop.PropertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                {
                    var propertyDataDict = prop.GetValue(value, null);
                    foreach (var item in propertyDataDict as IDictionary<string, object>)
                    {
                        if (!shouldIncludeProperty(returnedProperties, item.Key))
                        {
                            continue;
                        }
                        convertedObj.Add(item.Key, item.Value);
                    }

                    continue;
                }

                if (!shouldIncludeProperty(returnedProperties, prop.Name))
                {
                    continue;
                }

                var propValues = prop.GetValue(value, null);
                convertedObj.Add(prop.Name, propValues);
            }

            return base.BuildContent(convertedObj);
        }
    }
}