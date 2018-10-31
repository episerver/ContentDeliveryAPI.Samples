using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using System;
using System.Collections.Generic;
using System.Web;

namespace ContentDeliveryAPI.Samples.Customization
{
    /// <summary>
    /// Custom model mapper for doing our own transform content logic
    /// </summary>
    [ServiceConfiguration(typeof(IContentModelMapper), Lifecycle = ServiceInstanceScope.Singleton)]
    public class CustomContentModelMapper : DefaultContentModelMapper
    {
        public CustomContentModelMapper(IContentTypeRepository contentTypeRepository, ReflectionService reflectionService, IContentModelReferenceConverter contentModelService, IUrlResolver urlResolver, IEnumerable<IPropertyModelConverter> propertyModelConverters) : base(contentTypeRepository, reflectionService, contentModelService, urlResolver, propertyModelConverters)
        {
        }

        /// <summary>
        /// Here we try to get the personalization param from request and pass it to the transformer        
        /// </summary>        
        public override ContentApiModel TransformContent(IContent content, bool excludePersonalizedContent = false, string expand = "")
        {
            var personalization = HttpContext.Current.Request.Params["personalization"];
            /// (1) If personalization parameter is not passed along with the request or its value is 'false', exclude personalized content
            /// (2) If personalization parameter value is 'true', personalized content is included in return data
            return base.TransformContent(content, string.IsNullOrWhiteSpace(personalization) ? true : !Boolean.Parse(personalization), expand);
        }
    }
}