using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smartville.Models
{
    public class Sensor : BaseModel
    {
        public Sensor()
        {
            this.Statuses = new List<SensorStatus>();
            this.TimeZone = -3;
        }

        [Required]
        [StringLength(100)]
        public String Name { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public String SerialNumber { get; set; }

        [Required]
        public double TimeZone { get; set; }

        public String Address { get; set; }

        public City City { get; set; }

        public virtual ICollection<SensorStatus> Statuses { get; set; }
    }
}
