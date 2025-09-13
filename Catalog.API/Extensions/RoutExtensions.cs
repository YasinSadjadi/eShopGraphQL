using eShop.Catalog.Services;
using eShop.Catalog.Services.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eShop.Catalog.Extensions;

public static class RoutExtensions
{
    public static IEndpointRouteBuilder MapImageRoute(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(
            "/api/products/{id:int}/image",
            async Task<Results<NotFound, PhysicalFileHttpResult>> ([AsParameters] ImageParameters parametrs) =>
            {
                var imageInfo = await parametrs.GetImageInfoAsync();
                if (imageInfo is null)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.PhysicalFile(
                    imageInfo.Path,
                    imageInfo.MimeType,
                    lastModified: imageInfo.LastModified);

            }
            );
        return builder;
    }

    public record ImageParameters(
        int Id,
        ProductService ProductService,
        ImageStorage ImageStorage,
        IWebHostEnvironment Environment,
        CancellationToken ct)
    {
        public async Task<ImageInfo?> GetImageInfoAsync()
        {
            var product = await ProductService.GetProductByIdAsync(Id, ct);
            if (product?.ImageFileName is null)
            {
                return null;
            }

            var path = ImageStorage.GetFilePath(product.ImageFileName);

            if (path is null)
            {
                return null;
            }

            var extension = System.IO.Path.GetExtension(path);

            var mimeType = GetImageMimeTypeFromImageFileExtension(extension);

            var lastModified = File.GetLastWriteTime(path);

            return new ImageInfo(path, mimeType, lastModified);
        }

        private static string GetImageMimeTypeFromImageFileExtension(string extension)
        => extension switch
        {
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".bmp" => "image/bmp",
            ".tiff" => "image/tiff",
            ".wmf" => "image/wmf",
            ".jp2" => "image/jp2",
            ".svg" => "image/svg+xml",
            ".webp" => "image/webp",
            _ => "application/octet-stream"
        };
    }
    public record ImageInfo(string Path, string MimeType, DateTime LastModified);
}