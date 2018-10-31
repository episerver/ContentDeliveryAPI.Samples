using EPiServer.Core;

namespace ContentDeliveryAPI.Samples.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
