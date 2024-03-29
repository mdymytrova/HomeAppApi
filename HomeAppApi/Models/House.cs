﻿namespace HomeAppApi.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableUnits { get; set; }
        public bool Wifi { get; set; }
        public bool Laundry { get; set; }
        public string PhotoUrl { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
