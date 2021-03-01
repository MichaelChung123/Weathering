using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weathering.Models
{
    public class WeatherItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
