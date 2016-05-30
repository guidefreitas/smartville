using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartville.Models
{
    public class SensorStatus : BaseModel
    {
        public Sensor Sensor { get; set; }
        public SensorStatusType StatusType { get; set; }
        public String Value { get; set; }
    }
}
