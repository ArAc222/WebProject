using Example.Model;
using Example.Model.Common;
using System.Collections.Generic;

namespace Example.Service.Common
{
    public interface ICityService
    {
        List<CityModel> GetAllCities();
    }
}
