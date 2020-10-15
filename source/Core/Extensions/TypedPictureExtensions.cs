using System.Web;
using Umbraco.Web;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using System.Text.RegularExpressions;
using Umbraco.Core.Logging;
using System.Globalization;
using Our.Umbraco.NonProfitFramework.Core.Custom;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.NonProfitFramework.Core.Extensions
{
    public static class TypedPictureExtensions
    {
        #region Constructors
        /// <summary>
        /// Creates a new picture from an IPublishedContent to manipulate.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static TypedPictureElement Picture(this UmbracoHelper helper, IPublishedContent content)
        {
            return new TypedPictureElement(content);
        }

        /// <summary>
        /// Creates a new picture from an IPublishedContent to manipulate.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static TypedPictureElement Picture(this IPublishedContent content)
        {
            return new TypedPictureElement(content);
        }
        #endregion

        #region Linq
        /// <summary>
        /// Adds srcset to picture img elemet with specefied height and width.
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static TypedPictureElement Srcset(this TypedPictureElement picture, int width, int? height = null)
        {
            string croppedUrl = picture.Content.GetCropUrl(width: width, height: height, imageCropMode: ImageCropMode.Crop);

            picture.Srcset.Add(croppedUrl);

            return picture;
        }

        /// <summary>
        /// Adds srcset to img with specified width, height and device pixel ratio.
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="devicePixelRatioArgs"></param>
        /// <returns></returns>
        public static TypedPictureElement Srcset(this TypedPictureElement picture, int width, int? height = null, params double[] devicePixelRatioArgs)
        {
            var srcsets = new List<string>();
            foreach (double devicePixelRatio in devicePixelRatioArgs)
            {
                int newWidth = (int)(width * devicePixelRatio);
                int? newHeight = height.HasValue ? (int)(height.Value * devicePixelRatio) : height;

                picture.Srcset.Add(picture.GetCropUrl(newWidth, newHeight, devicePixelRatio));
            }

            return picture;
        }

        /// <summary>
        /// Adds new source element with specified media, width and or height.
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="media"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static TypedPictureElement Source(this TypedPictureElement picture, string media, int width, int? height = null)
        {
            picture.Sources.Add(new SourceElement {
                Media = media,
                Srcset = new List<string> {
                    { picture.GetCropUrl(width, height) }
                }
            });

            return picture;
        }

        /// <summary>
        /// Adds new source element with specified media, width and or height, times following device pixel ratio's
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="media"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="devicePixelRatioArgs"></param>
        /// <returns></returns>
        public static TypedPictureElement Source(this TypedPictureElement picture, string media, int width, int? height = null, params double[] devicePixelRatioArgs)
        {
            if (picture.Content == null)
                throw new System.ArgumentNullException("Content", "Missing Content from Picture. Use Umbraco.Picture(IPublishedContent)");

            var srcsets = new List<string>();
            foreach (double devicePixelRatio in devicePixelRatioArgs)
            {
                int newWidth = (int)(width * devicePixelRatio);
                int? newHeight = height.HasValue ? (int?)(height.Value * devicePixelRatio) : height;

                srcsets.Add(picture.GetCropUrl(newWidth, newHeight, devicePixelRatio));
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
