using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Composing;
using Umbraco.Core;

namespace Our.Umbraco.NonProfitFramework.Core.Custom
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class RegisterSiteServiceComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<ISiteService, SiteService>(Lifetime.Singleton);
        }
    }
}