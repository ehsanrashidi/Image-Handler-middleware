# Image Middleware
This is an ASP.NET Core middleware that handles and resizes image requests. It allows you to crop or resize images on-the-fly based on query-string parameters in the request URL.

Installation
To use the Image Middleware in your ASP.NET Core application, you can add the following line of codes inside of Program.cs

<pre>
  app.UseMiddleware<ImageMiddleware>();
  app.UseStaticFiles();
</pre>

and add ImageMiddleware class to your project that is inside Middleware folder

# Options
The following query-string parameters are available when requesting an image:

width: The width of the image to resize or crop.
height: The height of the image to resize or crop.
x: The operation to perform on the image (either "resize" or "crop").

# Examples
and finaly whenerver you send request to static image file with special parameters this middleware will be invoked
here is examples :

1- crop : https://localhost:7130/Images/image.jpg?x=crop&width=100&height=100

2- resize : https://localhost:7130/Images/image.jpg?width=150&height=300

# License
This middleware is licensed under the MIT License. See the LICENSE file for more information.

# Credits
This middleware was created by Ehsan Rashidi.
