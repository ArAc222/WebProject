using Example.Model;
using Example.Model.Common;
using Example.Repository.Common;
using Example.Service.Common;
using System.Collections.Generic;

namespace Example.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public List<CityModel> GetAllCities()
        {
            return _cityRepository.GetAllCities();
        }
    }
}
