using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umbraco.Core.Models;

namespace Our.Umbraco.NonProfitFramework.Core.Custom
{
    public class PictureElement : IPictureElement
    {
        #region properties
        public List<ISourceElement> Sources { get; set; }

        public string _alt = "";
        /// <summary>
        /// The alt attribute of the child HTMLImageElement
        /// </summary>
        public string Alt {
            get { return _alt; }
            set { _alt = value; }
        }
        
        private string _src = "data:image/gif;base64,R0lGODlhAQABAAAAADs=";
        /// <summary>
        /// The src attribute of the child HTMLImageElement
        /// </summary>
        public string Src
        {
            get { return _src; }
            set { _src = value; }
        }

        /// <summary>
        /// The srcset attribute of the child HTMLImageElement
        /// </summary>
        public ICollection<string> Srcset { get; set; }

        private bool _ie8 = false;
        /// <summary>
        /// Wraps HTMLSourceElements in HTMLVideoElement
        /// </summary>
        public bool IE8
        {
            get { return _ie8; }
            set { _ie8 = value; }
        }

        /// <summary>
        /// HTML Attributes of the HTMLPictureElement
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; }
        #endregion

        #region Constructors
        public PictureElement()
        {
            Sources = new List<ISourceElement>();
            Srcset = new List<string>();
            Attributes = new Dictionary<string, string>();
        }
        #endregion

        /// <summary>
        /// Return html markup for PictureElement
        /// </summary>
        /// <returns><picture {1}>{0}</picture></returns>
        public override string ToString()
        {
            string picture = "<picture {1}>{0}</picture>";
            List<string> attrs = this.Attributes.Select(p => (p.Key + "=\"" + p.Value + "\"")).ToList();
            return string.Format(picture, this.GetSources(), string.Join(" ", attrs));
        }

        private string GetSources()
        {
            StringBuilder sb = new StringBuilder();

            if(this.IE8) sb.Append("<!--[if IE 9]><video style=\"display: none;\"><![endif]-->");

            foreach (var source in this.Sources)
            {
                sb.Append(source.ToString());
            }

            if(this.IE8) sb.Append("<!--[if IE 9]></video><![endif]-->");
            
            sb.AppendFormat("<img src=\"{0}\" srcset=\"{1}\" alt=\"{2}\" />", this.Src, string.Join(",", this.Srcset), this.Alt);

            return sb.ToString();
        }
    }
}
