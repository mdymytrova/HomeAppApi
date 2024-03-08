namespace HomeAppApi.Models
{
    public class HouseOwner
    {
        public int HouseId { get; set; }
        public int OwnerId { get; set; }
        public House House { get; set; }
        public Owner Owner { get; set; }
    }
}
