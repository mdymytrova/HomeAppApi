namespace HomeAppApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<HouseCategory> HouseCategories { get; set; }
    }
}
