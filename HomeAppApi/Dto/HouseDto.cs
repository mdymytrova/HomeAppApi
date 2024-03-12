namespace HomeAppApi.Dto
{
    public class HouseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableUnits { get; set; }
        public bool Wifi { get; set; }
        public bool Laundry { get; set; }
        public string PhotoUrl { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public int StateId { get; set; }
        public string StateName { get; set; } = string.Empty;
        public int OwnerId { get; set; }
    }
}
