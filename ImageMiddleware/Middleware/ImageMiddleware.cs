using ImageResizer.Helpers;

namespace ImageResizer.Middleware
{
    public class ImageMiddleware
    {
        private readonly RequestDelegate _next;

        public ImageMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // Check if the request path is for a static image file
            if (context.Request.Path.Value.StartsWith("/Images/") && context.Request.Path.Value.EndsWith(".jpg"))
            {

                string widthQuery = context.Request.Query["width"];
                string heightQuery = context.Request.Query["height"];
                string operation = context.Request.Query["x"];

                // Get the file path from the request path
                string filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    context.Request.Path.Value.TrimStart('/'));

                // Check if the file exists
                if (File.Exists(filePath))
                {

                    // Read the image data from disk
                    byte[] imageData = File.ReadAllBytes(filePath);

                    // Crop the image
                    int x = 0; // x-coordinate of top-left corner of crop area
                    int y = 0; // y-coordinate of top-left corner of crop area

                    // width of crop area
                    int.TryParse(widthQuery, out int width);
                    if (width == 0) width = 100;

                    // height of crop area
                    int.TryParse(heightQuery, out int height);
                    if (height == 0) height = 100;
                    context.Response.ContentType = "image/jpeg";

                    if (operation == "crop")
                    {
                        byte[] croppedImageData = ImageHandler.CropImage(imageData, x, y, width, height);

                        // Write the cropped image data to the response stream
                        context.Response.ContentLength = croppedImageData.Length;
                        await context.Response.Body.WriteAsync(croppedImageData, 0, croppedImageData.Length);
                    }
                    else
                    {
                        // Write the resized image data to the response stream
                        byte[] resizedImageData = ImageHandler.ResizeImage(imageData, width, height);
                        context.Response.ContentLength = resizedImageData.Length;
                        await context.Response.Body.WriteAsync(resizedImageData, 0, resizedImageData.Length);

                    }
                    return;
                }
            }

            // If the request is not for a static image file, pass it to the next middleware
            await _next(context);
        }
    }
}
