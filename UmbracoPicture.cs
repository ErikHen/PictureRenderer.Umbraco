using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using PictureRenderer.Profiles;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Extensions;

namespace PictureRenderer.Umbraco
{
    public static class UmbracoPicture
    {
        public static HtmlString Picture(this IHtmlHelper helper, MediaWithCrops mediaWithCrops, PictureProfileBase profile, LazyLoading lazyLoading, string cssClass = "")
        {
            return Picture(helper, mediaWithCrops, profile, string.Empty, lazyLoading, cssClass);
        }

        public static HtmlString Picture(this IHtmlHelper helper, MediaWithCrops mediaWithCrops, PictureProfileBase profile, string altText, string cssClass)
        {
            return Picture(helper, mediaWithCrops, profile, altText, LazyLoading.Browser, cssClass);
        }

        public static HtmlString Picture(this IHtmlHelper helper, MediaWithCrops mediaWithCrops, PictureProfileBase profile, string altText = "", LazyLoading lazyLoading = LazyLoading.Browser, string cssClass = "")
        {
            if (mediaWithCrops == null)
            {
                return new HtmlString(string.Empty);
            }

            if (string.IsNullOrEmpty(altText) && !string.IsNullOrEmpty((string)mediaWithCrops.Content.Value("pictureAltText")))
            {
                altText = (string) mediaWithCrops.Content.Value("pictureAltText");
            }

            if (mediaWithCrops.LocalCrops.HasFocalPoint())
            {
                return Picture(helper, mediaWithCrops.LocalCrops, profile, altText, lazyLoading);
            }
           
            return Picture(helper, (ImageCropperValue)mediaWithCrops.Content.Value("umbracofile"), profile, altText, lazyLoading, cssClass);
        }


        public static HtmlString Picture(this IHtmlHelper helper, ImageCropperValue imageCropper, PictureProfileBase profile, LazyLoading lazyLoading, string cssClass = "")
        {
            return Picture(helper, imageCropper, profile, string.Empty, lazyLoading, cssClass);
        }

        public static HtmlString Picture(this IHtmlHelper helper, ImageCropperValue imageCropper, PictureProfileBase profile, string altText, string cssClass)
        {
            return Picture(helper, imageCropper, profile, altText, LazyLoading.Browser, cssClass);
        }

        public static HtmlString Picture(this IHtmlHelper helper, ImageCropperValue imageCropper, PictureProfileBase profile, string altText = "", LazyLoading lazyLoading = LazyLoading.Browser, string cssClass = "")
        {
            if (imageCropper == null)
            {
                return new HtmlString(string.Empty);
            }

            (double x, double y) focalPoint = default;
            if (imageCropper.HasFocalPoint())
            {
                focalPoint.x = decimal.ToDouble(imageCropper.FocalPoint.Left);
                focalPoint.y = decimal.ToDouble(imageCropper.FocalPoint.Top);
            }
            return new HtmlString(PictureRenderer.Picture.Render(imageCropper.ToString(), profile, altText, lazyLoading, focalPoint, cssClass));
        }

    }
}
