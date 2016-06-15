using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Things
{
    public abstract class Thing
    {
        public int Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        [NotMapped]
        public Type Type { get; set; }
    }
}
