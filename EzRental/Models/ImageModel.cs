namespace EzRental.Models
{
    public class Image
    {
        IFormFile File { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }

}
