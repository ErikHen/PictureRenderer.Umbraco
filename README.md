# PictureRenderer.Umbraco

[PictureRenderer](https://github.com/ErikHen/PictureRenderer) for Umbraco CMS. 
<br><br>
Render Umbraco images using a picture HTML element. CMS editors don't have to care about image sizes or formats. 
The most optimal image will always be used depending on the capabilities, screen size, and pixel density of the device that is used when visiting your web site.
<br>
PictureRenderer.Umbraco supports using a focal point when cropping/resizing, as well as getting image alt attribute from the Media object.
<br><br>
If you are unfamiliar with the details of the Picture element i highly recommed reading
 [this](https://webdesign.tutsplus.com/tutorials/quick-tip-how-to-use-html5-picture-for-responsive-images--cms-21015) and/or [this](https://www.smashingmagazine.com/2014/05/responsive-images-done-right-guide-picture-srcset/).

## How to use
* Add [PictureRenderer.Umbraco](https://www.nuget.org/packages/PictureRenderer.Umbraco/) to your Umbraco 9+ solution.
* Create Picture profiles for the different types of images that you have on your web site. A Picture profile describes how an image should be scaled in various cases. <br>
You could for example create Picture profiles for: "Top hero image", "Teaser image", "Image gallery thumbnail".
* Render picture elements with the Picture Html helper.


#### Define picture profiles

```
public static class PictureProfiles
{
    // Sample image
    // Up to 640 pixels viewport width, the picture width will be 100% of the viewport minus 40 pixels.
    // Up to 1200 pixels viewport width, the picture width will be 320 pixels.
    // On larger viewport width, the picture width will be 750 pixels.
    // Note that picture width is not the same as image width (but it can be, on screens with a "device pixel ratio" of 1).
    public static readonly ImageSharpProfile SampleImage = new() 
        {
            SrcSetWidths = new[] { 320, 640, 750, 1500 },
            Sizes = new[] { "(max-width: 640px) 100vw", "(max-width: 1200px) 320px", "750px" },
            AspectRatio = 1.777 // 16:9 = 16/9 = 1.777
        };

    // Top hero
    // Picture width is always 100% of the viewport width.
    public static readonly ImageSharpProfile TopHero = new()
        {
            SrcSetWidths = new[] { 1024, 1366, 1536, 1920 },
            Sizes = new[] { "100vw" },
            AspectRatio = 2
        };

    // Thumbnail
    // Thumbnail is always 150px wide. But the browser may still select the 300px image for a high resolution screen (e.g. mobile or tablet screens).
    public static readonly ImageSharpProfile Thumbnail = new()
        {
            SrcSetWidths = new[] { 150, 300 },
            Sizes = new[] { "150px" },
            AspectRatio = 1  //square image (equal height and width).
        };
}
```
* **SrcSetWidths** - The different image widths you want the browser to select from. These values are used when rendering the srcset attribute.
* **Sizes** - Define the size (width) the image should be according to a set of "media conditions" (similar to css media queries). Values are used to render the sizes attribute.
* **AspectRatio (optional)** - The wanted aspect ratio of the image (width/height). Ex: An image with aspect ratio 16:9 = 16/9 = 1.777.
* **Quality (optional)** - Image quality. Lower value = less file size. Not valid for all image formats. Deafult value: 80.
* **FallbackWidth (optional)** - Image width for browsers without support for picture element. Will use the largest SrcSetWidth if not set.
* **ImageDecoding (optional)** - Value for img element `decoding` attribute. Default value: `async`.
* **ImgWidthHeight (optional)** - If true, `width` and `height` attributes will be rendered on the img element.
<br><br>

#### Render picture element for "Image1". 

```@Html.Picture(Model.Image1, PictureProfiles.Thumbnail)```
##### Parameters
* **mediaWithCrops/imageCropper** - Media or Image cropper object.
* **profile** - The Picture profile that specifies image widths, etc..
* **altText (optional)** - Will overide alt text set on Media.
* **lazyLoading (optional)** - Type of lazy loading. Currently only [browser native lazy loading](https://developer.mozilla.org/en-US/docs/Web/Performance/Lazy_loading#images_and_iframes) (or none).
* **cssClass (optional)** - Css class for img element.
<br>

The result would be something like this
```
<picture>
<source srcset="
 /media/34fjcmcr/my-image.jpg?width=150&height=150&rxy=0.254%2c0.342&quality=80 150w,
 /media/34fjcmcr/my-image.jpg?width=300&height=300&rxy=0.254%2c0.342&quality=80 300w"
 sizes="150px"  />
<img alt="" src="/media/34fjcmcr/my-image.jpg?width=300&height=300&rxy=0.254%2c0.342&quality=80" loading="lazy" />
</picture>
```

See also the [Umbraco sample project](https://github.com/ErikHen/PictureRenderer.Samples/tree/master/Umbraco9-rc)
<br><br>
## WebP images
WebP versions of the images will be added to the rendered Picture element (from v2). 
[browsers that supports WebP](https://caniuse.com/webp) will have significally improved download size.

## Focal point 
Images will be cropped if the aspect ratio defined in the picture profile is not the same as the image origin. 
To make sure that the main object in your image is still visible, you can use a focal point.

## Alt text
You can add a field/property on the Image type called "pictureAltText". 
This value will be used when rendering the alt text in the `<img>` element.<br>
![](https://raw.githubusercontent.com/ErikHen/PictureRenderer.Umbraco/main/_Build/alt_text_property.png)


<br><br>
## Version history
**2.0** WebP support. Targeting Umbraco 10 and .Net6. <br>
**1.2** Possible to render `class`, `decoding`, and `width` + `height` attributes on the `<img>` element. <br>
**1.1** Update reference to stable Umbraco 9.0.0 . <br>
**1.0** First version. Build for release candidate of Umbraco 9. <br>
