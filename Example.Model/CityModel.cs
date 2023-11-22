using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Model.Common;

namespace Example.Model
{
    public class CityModel : ICityModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
