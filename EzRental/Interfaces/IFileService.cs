namespace EzRental.Interfaces
{
    public interface IFileService
    {
        Task<bool> SaveImages(IFormFileCollection fileCollection, string directory);

        Task<List<string>> GetImagePaths(string directory);

    }
}
