using LightInject;
using Our.Umbraco.NonProfitFramework.Core.Custom;
using Our.Umbraco.NonProfitFramework.Core.Models;
using Umbraco.Core;
using UW = Umbraco.Web;

namespace Our.Umbraco.NonProfitFramework.Core.Extensions
{
    public static class UmbracoHelperExtensions
    {
        public static Website Website(this UW.UmbracoHelper umbracoHelper)
        {
            
            var siteService = UW.Composing.Current.Factory.GetInstance<ISiteService>();

            return siteService.GetWebsiteById(umbracoHelper.AssignedContentItem.Id);
        }

    }
}
