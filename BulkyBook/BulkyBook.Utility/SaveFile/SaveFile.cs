using BulkyBook.Utility.SaveFile.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BulkyBook.Utility.SaveFile;
public class SaveFile : ISaveFile
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private string _lastSavedFilePath = string.Empty;

    public SaveFile(IWebHostEnvironment webHostEnvironment) => _webHostEnvironment = webHostEnvironment;

    public string GetLastSavedFilePath() => _lastSavedFilePath;

    public void Save(IFormFile formFile, string uploadPath)
    {
        if (formFile == null) throw new ArgumentNullException(nameof(formFile));

        var wwwRootPath = _webHostEnvironment.WebRootPath;
        var fileName = Guid.NewGuid().ToString();
        var fullUploadPath = Path.Combine(wwwRootPath, uploadPath);
        var extension = Path.GetExtension(formFile.FileName);

        using var filestream = new FileStream(Path.Combine(fullUploadPath, fileName + extension), FileMode.Create);
        formFile.CopyTo(filestream);
        _lastSavedFilePath = @"\" + uploadPath + @"\" + fileName + extension;
    }

    public void Update(IFormFile formFile, string currentFilePath, string uploadPath)
    {
        if (formFile == null) throw new ArgumentNullException(nameof(formFile));
        
        Delete(currentFilePath);
        Save(formFile, uploadPath);
    }

    public void Delete(string filePath)
    {
        var wwwRootPathToCheck = _webHostEnvironment.WebRootPath;
        var fullFilePath = Path.Combine(wwwRootPathToCheck, filePath.TrimStart('\\'));

        if (File.Exists(fullFilePath))
            File.Delete(fullFilePath);
    }
}
