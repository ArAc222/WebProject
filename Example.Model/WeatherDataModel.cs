using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Model.Common;

namespace Example.Model
{
    public class WeatherDataModel : IWeatherDataModel
    {
        public int DataId { get; set; }
        public int CityId { get; set; }
        public int Temperature { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
