using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Our.Umbraco.NonProfitFramework.Core.Custom
{
    public class SourceElement : ISourceElement
    {
        public ICollection<string> Srcset { get; set; }

        public string Media { get; set; }

        public string Type { get; set; }

        public string Sizes { get; set; }

        public override string ToString()
        {
            var tagBuilder = new TagBuilder("source");
            tagBuilder.MergeAttribute("media", this.Media);
            tagBuilder.MergeAttribute("srcset", string.Join(",", this.Srcset));
            if (!String.IsNullOrEmpty(this.Sizes))
            {
                tagBuilder.MergeAttribute("sizes", this.Sizes);
            }
            return tagBuilder.ToString();
        }

        #region contructors
        public SourceElement()
        {
            Srcset = new List<string>();
        }
        #endregion
    }
}
