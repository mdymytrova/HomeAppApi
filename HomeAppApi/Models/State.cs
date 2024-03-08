namespace HomeAppApi.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<House> Houses { get; set; }
    }
}
