using Example.Model;
using Example.Model.Common;
using System.Collections.Generic;

namespace Example.Repository.Common
{
    public interface ICityRepository
    {
        List<CityModel> GetAllCities();
    }
}
