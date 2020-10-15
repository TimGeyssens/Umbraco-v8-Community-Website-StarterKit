using System.Web;
using System.Web.Mvc;
using Umbraco.Web;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using System.Text.RegularExpressions;
using System;
using Our.Umbraco.NonProfitFramework.Core.Custom;

namespace Our.Umbraco.NonProfitFramework.Core.Extensions
{
    public static class PictureExtensions {
        /// <summary>
        /// Creates an empty picture element.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IPictureElement Picture(this UmbracoHelper helper) {
            return new PictureElement();
        }

        /// <summary>
        /// Creates an empty picture element.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IPictureElement Picture(this HtmlHelper helper) {
            return new PictureElement();
        }

        /// <summary>
        /// Returns the html of picture element with sources and attributes.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        public static IHtmlString RenderPicture(this HtmlHelper helper, IPictureElement picture) {
            return new HtmlString(picture.ToString());
        }

        #region Linq
        /// <summary>
        /// Adds the following attributes and values to picture element.
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public static IPictureElement Attrs(this IPictureElement picture, Dictionary<string, string> attrs) {
            picture.Attributes = attrs;
            return picture;
        }

        /// <summary>
        /// Adds the following attribute and value to picture element.
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPictureElement Attr(this IPictureElement picture, string key, string value) {
            picture.Attributes.Add(key, value);
            return picture;
        }

        /// <summary>
        /// You should leave this empty
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public static IPictureElement Src(this IPictureElement picture, string src) {
            picture.Src = src;
            return picture;
        }

        /// <summary>
        /// Add multiple sources in the srcset attribute on img element
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="srcset"></param>
        /// <returns></returns>
        public static IPictureElement Srcset(this IPictureElement picture, params string[] srcset) {
            picture.Srcset = srcset;
            return picture;
        }

        /// <summary>
        /// Adds new source with specified media and srcsets.
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="media"></param>
        /// <param name="srcsets"></param>
        /// <returns></returns>
        public static IPictureElement Source(this IPictureElement picture, string media, params string[] srcsets) {
            foreach (string src in srcsets) {
                picture.Sources.Add(new SourceElement { Media = media, Srcset = srcsets });
            }
            return picture;
        }

        /// <summary>
        /// Sets the alt attribute on the img element.
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public static IPictureElement Alt(this IPictureElement picture, string alt) {
            picture.Alt = alt;
            return picture;
        }

        /// <summary>
        /// Convert to HtmlString
        /// </summary>
        /// <param name="picture"></param>
        /// <returns>Picture element as HtmlString</returns>
        public static IHtmlString Html(this IPictureElement picture) {
            return new HtmlString(picture.ToString());
        }

        
        #endregion
    }
}
