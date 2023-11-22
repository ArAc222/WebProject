using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Service.Common;
using Example.Model;
using Example.Repository;

namespace Example.Service
{
    public class WeatherDataService : IWeatherDataService
    {
        private readonly IWeatherDataRepository _weatherDataRepository;

        public WeatherDataService(IWeatherDataRepository weatherDataRepository)
        {
            _weatherDataRepository = weatherDataRepository;
        }

        public List<WeatherDataModel> GetAllWeatherDataByCityName(string cityName)
        {
            return _weatherDataRepository.GetAllWeatherDataByCityName(cityName);
        }

        public void AddWeatherData(WeatherDataModel weatherData)
        {
            _weatherDataRepository.AddWeatherData(weatherData);
        }

        public void UpdateWeatherData(string cityName, WeatherDataModel updatedData)
        {
            _weatherDataRepository.UpdateWeatherData(cityName, updatedData);
        }

        public void DeleteCityAndWeatherData(string cityName)
        {
            _weatherDataRepository.DeleteCityAndWeatherData(cityName);
        }
    }
}

