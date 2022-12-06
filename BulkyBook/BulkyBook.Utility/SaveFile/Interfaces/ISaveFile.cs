using Microsoft.AspNetCore.Http;

namespace BulkyBook.Utility.SaveFile.Interfaces;
public interface ISaveFile
{
    void Save(IFormFile formFile, string uploadPath);
    string GetLastSavedFilePath();
}
