namespace HomeAppApi.Models
{
    public class HouseCategory
    {
        public int HouseId { get; set; }
        public int CategoryId { get; set; }
        public House House { get; set; }
        public Category Category { get; set; }
    }
}
