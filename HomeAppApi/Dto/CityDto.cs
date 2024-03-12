namespace HomeAppApi.Dto
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; } = string.Empty;
        public int HouseCount { get; set; }
    }
}
