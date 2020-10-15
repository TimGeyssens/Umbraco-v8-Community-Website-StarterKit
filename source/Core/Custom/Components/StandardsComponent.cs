using System.Web.Security;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;
using System.Linq;
using System;
using Umbraco.Core.Security;
using Umbraco.Web.Editors;
using Umbraco.Web.PublishedModels;

namespace Our.Umbraco.NonProfitFramework.Core.Custom
{
   
  
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class StandardsComposer : ComponentComposer<StandardsComponent>
    { }

    public class StandardsComponent : IComponent
    {

        public void Initialize()
        {
            EditorModelEventManager.SendingContentModel += EditorModelEventManager_SendingContentModel1;
                      
        }

        private void EditorModelEventManager_SendingContentModel1(System.Web.Http.Filters.HttpActionExecutedContext sender, EditorModelEventArgs<global::Umbraco.Web.Models.ContentEditing.ContentItemDisplay> e)
        {
            var identity = (UmbracoBackOfficeIdentity)System.Web.HttpContext.Current.User.Identity;
            var currentUSer = Current.Services.UserService.GetByProviderKey(identity.Id);

            var canSeeAdminTab = currentUSer.Groups.Any(x => x.Alias == "admin");

            if (!canSeeAdminTab)
            {
                foreach (var variant in e.Model.Variants)
                    variant.Tabs = variant.Tabs.Where(x => x.Label != "Admin");
            }
        }

        public void Terminate()
        {
            
        }
    }
}