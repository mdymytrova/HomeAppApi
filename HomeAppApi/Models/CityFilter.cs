namespace HomeAppApi.Models
{
    public class CityFilter
    {
        public bool includeWithoutHouses { get; set; }
        public string search { get; set; } = string.Empty;
    }
}
