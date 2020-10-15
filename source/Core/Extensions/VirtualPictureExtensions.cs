
using Umbraco.Web;
using System.Collections.Generic;
using Our.Umbraco.NonProfitFramework.Core.Custom;

namespace Our.Umbraco.NonProfitFramework.Core.Extensions
{
    public static class VirtualPictureExtensions
    {
        #region Constructors
        /// <summary>
        /// Creates a new picture element with the specified virtual source to manipulate.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public static VirtualPictureElement Picture(this UmbracoHelper helper, string src)
        {
            // TODO: add logic to surfix with ~/
            return new VirtualPictureElement(src);
        }
        #endregion

        #region Linq
        /// <summary>
        /// Adds src to picture img element
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static VirtualPictureElement Srcset(this VirtualPictureElement picture, int width, int? height = null)
        {
            string croppedUrl = string.Format("{0}?width={1}", picture.VirtualPath, width);

            if (height.HasValue)
                croppedUrl += "&height=" + height.Value;

            croppedUrl += "&mode=crop";

            picture.Srcset.Add(croppedUrl);

            return picture;
        }

        /// <summary>
        /// Adds src with Device pixel ratio to img element
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="devicePixelRatio"></param>
        /// <returns></returns>
        public static VirtualPictureElement Srcset(this VirtualPictureElement picture, int width, int? height = null, params double[] devicePixelRatio)
        {
            if (string.IsNullOrEmpty(picture.VirtualPath))
                throw new System.ArgumentNullException("VirtualPath", "Missing VirtualPath from Picture. Use Umbraco.Picture(String)");

            var srcsets = new List<string>();
            foreach (double ratio in devicePixelRatio)
            {
                int newWidth = (int)(width * ratio);
                int? newHeight = height.HasValue ? (int)(height.Value * ratio) : height;

                string croppedUrl = string.Format("{0}?width={1}", picture.VirtualPath, newWidth);

                if (newHeight.HasValue)
                    croppedUrl += "&height=" + newHeight.Value;

                croppedUrl += "&mode=crop";
                croppedUrl += " x" + string.Format("{0:0.##}", ratio).Replace(',', '.');

                picture.Srcset.Add(croppedUrl);
            }

            return picture;
        }

        /// <summary>
        /// Adds new source child to picture element with media and srcset
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="media"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static VirtualPictureElement Source(this VirtualPictureElement picture, string media, int width, int? height = null)
        {
            if (string.IsNullOrEmpty(picture.VirtualPath))
                throw new System.ArgumentNullException("VirtualPath", "Missing VirtualPath from Picture. Use Umbraco.Picture(IPublishedContent)");

            string croppedUrl = string.Format("{0}?width={1}", picture.VirtualPath, width);

            if (height.HasValue)
                croppedUrl += "&height=" + height.Value;

            croppedUrl += "&mode=crop";

            picture.Sources.Add(new SourceElement {
                Media = media,
                Srcset = new List<string> {
                    { croppedUrl }
                }
            });
            return picture;
        }

        /// <summary>
        /// Adds new source to child to picture element with media, srcset and device pixel ratio
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="media"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="devicePixelRatio"></param>
        /// <returns></returns>
        public static VirtualPictureElement Source(this VirtualPictureElement picture, string media, int width, int? height, params double[] devicePixelRatio)
        {
            if (string.IsNullOrEmpty(picture.VirtualPath))
                throw new System.ArgumentNullException("VirtualPath", "Missing VirtualPath from Picture. Use Umbraco.Picture(String)");

            var srcsets = new List<string>();
            foreach (double pixelRatio in devicePixelRatio)
            {
                if (pixelRatio <= 0.9) continue;

                int newWidth = (int)(width * pixelRatio);
                int? newHeight = height.HasValue ? (int?)(height.Value * pixelRatio) : height;

                string croppedUrl = string.Format("{0}?width={1}&height={2}&mode=crop", picture.VirtualPath, newWidth, newHeight);
                srcsets.Add(string.Format("{0} {1}", croppedUrl, string.Format("{0:0.##}x", pixelRatio).Replace(',', '.')));
            }

            picture.Sources.Add(new SourceElement
            {
                Media = media,
                Srcset = srcsets
            });
            return picture;
        }
        #endregion
    }
}
