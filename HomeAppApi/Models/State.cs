namespace HomeAppApi.Models
{
    public class State
    {
        public int StateId { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<House> Houses { get; set; }
    }
}
