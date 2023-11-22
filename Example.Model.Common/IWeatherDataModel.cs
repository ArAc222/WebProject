using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Model.Common
{
    public interface IWeatherDataModel
    {
        int DataId { get; set; }
        int CityId { get; set; }
        int Temperature { get; set; }
        string Description { get; set; }
        DateTime Date { get; set; }
    }
}
