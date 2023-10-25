namespace EzRental.Models
{
    public class AdvertisementWrapper
    {
        public Advertisement? advertisement { get; set; }
        public Room? room { get; set; }
        public User? renter { get; set; }
        public List<Facilties>? facilties { get; set; }
    }
}
