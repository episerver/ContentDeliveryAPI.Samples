using EPiServer.Cms.Shell;
using EPiServer.ContentApi.Core;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Editor;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using EPiServer.Web.Routing;
using MusicFestival.Template.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicFestival.Template.Models
{
    [ServiceConfiguration(typeof(IContentModelMapper))]
    public class ExtendedContentModelMapper : ContentModelMapperBase
    {
        private readonly ServiceAccessor<HttpContextBase> _httpContextAccessor;
        private readonly IContentVersionRepository _versionRepository;

        [Obsolete]
        public ExtendedContentModelMapper(
            IContentTypeRepository contentTypeRepository,
            ReflectionService reflectionService,
            IContentModelReferenceConverter contentModelService,
            IUrlResolver urlResolver,
            IEnumerable<IPropertyModelConverter> propertyModelConverters,
            IContentVersionRepository contentVersionRepository,
            ContentLoaderService contentLoaderService,
            ServiceAccessor<HttpContextBase> httpContextAccessor,
            IContentVersionRepository versionRepository) : base(
                contentTypeRepository,
                reflectionService,
                contentModelService,
                urlResolver,
                propertyModelConverters,
                contentVersionRepository,
                contentLoaderService)
        {
            _httpContextAccessor = httpContextAccessor;
            _versionRepository = versionRepository;
        }

        public override int Order
        {
            get
            {
                return 110;
            }
        }

        /// <summary>
        /// Maps an instance of IContent to ContentApiModel and additionally add info about existing languages
        /// </summary>
        public override ContentApiModel TransformContent(IContent content, bool excludePersonalizedContent, string expand)
        {
            var contentModel = base.TransformContent(content, excludePersonalizedContent, expand);
            contentModel.Url = ResolveUrl(content.ContentLink, content.LanguageBranch());

            contentModel.Properties = contentModel.Properties.Select(FlattenProperty).ToDictionary(x => x.Key, x => x.Value);

            if (contentModel.ParentLink != null)
            {
                contentModel.ParentLink.Url = ResolveUrl(content.ParentLink, content.LanguageBranch());
            }

            return contentModel;
        }

        protected override string ResolveUrl(ContentReference contentLink, string language)
        {
            return _urlResolver.GetUrl(contentLink, language, new UrlResolverArguments
            {
                ContextMode = GetContextMode()
            });
        }

        private static KeyValuePair<string, object> FlattenProperty(KeyValuePair<string, object> property)
        {
            return new KeyValuePair<string, object>(property.Key, FlattenPropertyValue(property.Value));
        }

        /// <summary>
        /// Extract the actual value from the property model.
        /// In our simple Vue app we are only interested about the property values,
        /// flattening the property model will simplify our client side components.
        /// </summary>
        private static object FlattenPropertyValue(object propertyValue)
        {
            switch (propertyValue)
            {
                case ContentAreaPropertyModel propertyModel:
                    return propertyModel?.ExpandedValue?.Select(x => ConvertContentAreaItem(x, propertyModel));
                case BuyTicketBlockPropertyModel propertyModel:
                    return propertyModel.Value;
                case PropertyModel<string, PropertyString> propertyModel:
                    return propertyModel.Value;
                case PropertyModel<string, PropertyUrl> propertyModel:
                    return propertyModel.Value;
                case PropertyModel<DateTime?, PropertyDate> propertyModel:
                    return propertyModel.Value;
                case PropertyModel<bool?, PropertyBoolean> propertyModel:
                    return propertyModel.Value;
                case PropertyModel<string, PropertyLongString> propertyModel:
                    return propertyModel.Value;
                case PropertyModel<string, PropertyXhtmlString> propertyModel:
                    return propertyModel.Value;
                case CategoryPropertyModel propertyModel:
                    return propertyModel.Value;
                default:
                    return propertyValue;
            }
        }

        /// <summary>
        /// We need to extend the model for content areas with available display options so our component will get a correct css class
        /// see how it's used in Assets/Scripts/components/ContentArea.vue
        /// </summary>
        private static object ConvertContentAreaItem(ContentApiModel contentApiModel, ContentAreaPropertyModel propertyModel)
        {
            var contentModelDisplayOption = propertyModel.Value.FirstOrDefault(x => x.ContentLink.Id == contentApiModel.ContentLink.Id)?.DisplayOption;
            contentApiModel.Properties.Add("displayOption", contentModelDisplayOption);
            return contentApiModel;
        }

        /// <summary>
        /// The "epieditmode" querystring parameter is added to URLs by Episerver as a way to keep track of what context is currently active.
        /// If there is no "epieditmode" parameter we're in regular view mode.
        /// If the "epieditmode" parameter has value "True" we're in edit mode.
        /// If the "epieditmode" parameter has value "False" we're in preview mode.
        /// All of these different modes will resolve to different URLs for the same content.
        /// </summary>
        private ContextMode GetContextMode()
        {
            var httpCtx = _httpContextAccessor();
            if (httpCtx == null || httpCtx.Request == null || httpCtx.Request.QueryString[PageEditing.EpiEditMode] == null)
            {
                return ContextMode.Default;
            }
            if (bool.TryParse(httpCtx.Request.QueryString[PageEditing.EpiEditMode], out bool editMode))
            {
                return editMode ? ContextMode.Edit : ContextMode.Preview;
            }
            return ContextMode.Undefined;
        }

        public override bool CanHandle<T>(T content)
        {
            return content is IContent;
        }
    }
}