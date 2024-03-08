namespace HomeAppApi.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableUnits {  get; set; }
        public bool Wifi {  get; set; }
        public bool Laundry { get; set; }
        public City City { get; set; }
        public State State { get; set; }
        public ICollection<HouseOwner> HouseOwners { get; set; }
        public ICollection<HouseCategory> HouseCategories { get; set; }
    }
}
