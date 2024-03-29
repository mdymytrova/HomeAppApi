﻿using System.Diagnostics.Metrics;

namespace HomeAppApi.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<House> Houses{ get; set; }
    }
}
