﻿namespace HomeAppApi.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<House> Houses { get; set; }
    }
}
