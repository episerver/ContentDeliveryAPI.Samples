using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;

namespace MusicFestival.Template.Models.Commerce
{
    [CatalogContentType(
        GUID = "a23da2a1-7843-4828-9322-c63e28059f6a",
        MetaClassName = "CategoryNode",
        DisplayName = "Category Node",
        Description = "Display categories")]
    public class CategoryNode : NodeContent
    {

    }
}