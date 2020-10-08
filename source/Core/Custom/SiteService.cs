using Our.Umbraco.NonProfitFramework.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace Our.Umbraco.NonProfitFramework.Core.Custom
{

    public class SiteService : ISiteService
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public SiteService(IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;

        }
        public Website GetWebsiteById(int id)
        {
            Website website = null;

            using (UmbracoContextReference umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
            {

                website = umbracoContextReference.UmbracoContext.Content.GetById(id).AncestorOrSelf(Website.ModelTypeAlias) as Website;
            }

            return website;
        }


        public Seo GetSeoDetailsById(int id)
        {
            Seo seo = null;

            using (UmbracoContextReference umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
            {

                seo = umbracoContextReference.UmbracoContext.Content.GetById(id) as Seo;
            }

            return seo;
        }


    }
}