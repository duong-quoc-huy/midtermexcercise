using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

public class MyTool
{
    public static string UploadImageToFolder(IFormFile file, string folder)
    {
        try
        {
            // Validate file extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Invalid file type.");

            // Validate file size (max 2MB)
            if (file.Length > 2 * 1024 * 1024)
                throw new InvalidOperationException("File size exceeds 2MB.");

            // Ensure folder exists
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Create safe, unique filename
            var uniqueName = Guid.NewGuid().ToString() + extension;
            var fullPath = Path.Combine(folderPath, uniqueName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/images/" + folder + "/" + uniqueName; // Return web-accessible path
        }
        catch
        {
            return string.Empty;
        }
    }
}
