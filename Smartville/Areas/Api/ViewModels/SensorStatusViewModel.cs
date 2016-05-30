using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartville.Areas.Api.ViewModels
{
    public class SensorStatusViewModel
    {
        public long StatusId { get; set; }
        public String SensorSerialNumber { get; set; }
        public long SensorId { get; set; }
        public int StatusType { get; set; }
        public String Value { get; set; }

    }
}
