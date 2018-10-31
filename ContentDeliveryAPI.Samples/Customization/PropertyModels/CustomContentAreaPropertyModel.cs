using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.Security;
using EPiServer.SpecializedProperties;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ContentDeliveryAPI.Samples.Customization
{
    /// <summary>
    /// Custom view model to mapped with <see cref="PropertyContentArea"/>
    /// </summary>
    public class CustomContentAreaPropertyModel : ContentAreaPropertyModel
    {
        public CustomContentAreaPropertyModel(
              PropertyContentArea propertyContentArea,
              bool excludePersonalizedContent) : base(propertyContentArea, excludePersonalizedContent)
        {

        }

        /// <summary>
        /// Herre we override Expand behaviour of this model and expands all level 
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        protected override IEnumerable<ContentApiModel> ExtractExpandedValue(CultureInfo language)
        {
            var expandedValue = new List<ContentApiModel>();

            var contentReferences = Value.Where(x => x.ContentLink != null).Select(x => new ContentReference(x.ContentLink.Id.Value));
            var content = _contentLoaderService.GetItems(contentReferences, language).ToList();

            var principal = ExcludePersonalizedContent ? _principalAccessor.GetAnonymousPrincipal() : _principalAccessor.GetCurrentPrincipal();
            var filteredContent = content.Where(x => _accessEvaluator.HasAccess(x, principal, AccessLevel.Read)).ToList();

            // passing '*' as expand parameter to expand all property level.
            // The default value is string.Empty and that's why on property level is expanded by default
            filteredContent.ForEach(x => expandedValue.Add(_contentModelMapper.TransformContent(x, ExcludePersonalizedContent, "*")));
            return expandedValue;
        }
    }
}