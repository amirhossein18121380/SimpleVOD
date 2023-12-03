namespace VOD.Service;
public interface IFileUploadService
{
    Task<string> UploadVideoAsync(IFormFile file);
    Task<string> UploadImageAsync(IFormFile file);
}

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _env;

    public FileUploadService(IWebHostEnvironment env)
    {
        _env = env ?? throw new ArgumentNullException(nameof(env));
    }
    private async Task<string> UploadFileAsync(IFormFile file, string folder, string[] allowedExtensions)
    {
        if (file == null || file.Length == 0)
        {
            return null; // No file to save
        }

        var uploadsFolder = Path.Combine(_env.WebRootPath, folder);
        EnsureFolderExists(uploadsFolder);

        var fileExtension = Path.GetExtension(file.FileName)?.ToLower();

        if (!allowedExtensions.Contains(fileExtension))
        {
            return null; // "Invalid file format";
        }

        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        // Return the URL of the saved file
        return $"/{folder}/{uniqueFileName}";
    }

    public async Task<string> UploadVideoAsync(IFormFile file)
    {
        string[] allowedVideoExtensions = { ".mp4", ".avi", ".mkv" };
        return await UploadFileAsync(file, "videos", allowedVideoExtensions);
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        string[] allowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        return await UploadFileAsync(file, "images", allowedImageExtensions);
    }
    private void EnsureFolderExists(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }
}