using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Web.Mvc;
using System.Web.Mvc;

namespace MusicFestival.Template.Controllers
{
    public class CatalogController : ContentController<CatalogContentBase>
    {
        //private readonly bool _isInEditMode;        

        [HttpGet]
        public ActionResult Index(CatalogContentBase currentContent, string entryCode = "", bool useQuickview = false, bool skipTracking = false)
        {
            return View();
        }

    }
}