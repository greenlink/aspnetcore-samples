using Microsoft.AspNetCore.Http;

namespace BulkyBook.Utility.SaveFile.Interfaces;
public interface ISaveFile
{
    void Save(IFormFile formFile, string uploadPath);
    void Update(IFormFile formFile, string oldPath, string uploadPath);
    string GetLastSavedFilePath();
}
