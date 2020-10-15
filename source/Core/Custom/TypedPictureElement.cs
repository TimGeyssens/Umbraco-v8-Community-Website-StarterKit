using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Our.Umbraco.NonProfitFramework.Core.Custom
{
    public class TypedPictureElement : PictureElement
    {
        public IPublishedContent Content { get; set; }
        private string PropertyAlias { get; set; }

        private ImageCropMode ImageCropMode { get; set; }

        #region Constructors
        public TypedPictureElement(IPublishedContent content, string propertyAlias = "umbracoFile", ImageCropMode imageCropMode = ImageCropMode.Crop)
            : base()
        {
            if (content == null)
                throw new System.ArgumentNullException("Content", "Missing Content from Picture. Use Umbraco.Picture(IPublishedContent)");

            ImageCropMode = imageCropMode;
            Content = content;
            PropertyAlias = propertyAlias;
        }
        #endregion

        public string GetCropUrl(int width, int? height, double? devicePixelRatio = null)
        {
            string url = this.Content.GetCropUrl(width: width, height: height, imageCropMode: this.ImageCropMode);

            if(devicePixelRatio.HasValue)
                url += " " + string.Format("{0:0.##}x", devicePixelRatio).Replace(',', '.');

            return url;
        }
    }
}
