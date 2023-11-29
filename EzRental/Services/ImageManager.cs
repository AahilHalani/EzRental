using EzRental.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net.NetworkInformation;
using System.Text;
namespace EzRental.Services
{
    public class ImageManager:IFileService
    {   
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageManager(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> SaveImages(IFormFileCollection images, string directory)
        {
            try
            {
                StringBuilder imagePath = new StringBuilder();

                string dirPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", directory);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                foreach (IFormFile image in images)
                {
                    //await Console.Out.WriteLineAsync(image.FileName);
                    imagePath.Append(Path.Combine(_webHostEnvironment.ContentRootPath, "Images", directory, image.FileName));
                    using (var fileStream = new FileStream(imagePath.ToString(), FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    imagePath.Clear();
                }
                return true;
            }
            catch(Exception e)
            {
                await Console.Out.WriteLineAsync(e.ToString());
                return false;
            }
        }

        public async Task<List<string>> GetImagePaths(string directory)
        {
            try 
            {
                string dirPath = Path.Combine(_webHostEnvironment.ContentRootPath, 
                    "Images", directory);
                var paths = Directory.EnumerateFiles(dirPath).ToList<string>();
                return paths;
            }
            catch(Exception e)
            {
                await Console.Out.WriteLineAsync(e.ToString());
                return new List<string>();
            }
        }
    }
}
