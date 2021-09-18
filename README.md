# PictureRenderer.Umbraco

[PictureRenderer](https://github.com/ErikHen/PictureRenderer) for Umbraco CMS. 
<br><br>
Render Umbraco images using a picture HTML element. CMS editors don't have to care about image sizes or formats. 
The most optimal image will always be used depending on the capabilities, screen size, and pixel density of the device that is used when visiting your web site.
<br><br>
PictureRenderer.Umbraco supports using a focal point when cropping/resizing, as well as getting image alt attribute from the Media object.

## How to use
* Add [PictureRenderer.Umbraco](https://www.nuget.org/packages/PictureRenderer.Umbraco/) to your Umbraco 9+ solution.
* Define the differnt Picture profiles that you want to use. _More info will be addedd..._
* Render picture elements with the Picture Html helper.

### Sample code
Render picture element for "Image1". 

```@Html.Picture(Model.Image1, PictureProfiles.SampleImage)```

<br>

See also the [Umbraco sample project](https://github.com/ErikHen/PictureRenderer.Samples/tree/master/Umbraco9-rc)

