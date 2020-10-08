using Our.Umbraco.NonProfitFramework.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.PublishedModels;

namespace Our.Umbraco.NonProfitFramework.Core.Custom
{

    public interface ISiteService
    {
        Website GetWebsiteById(int id);

        Seo GetSeoDetailsById(int id);

    }


}