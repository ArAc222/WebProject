using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Model;

namespace Example.Service.Common
{
    public interface IWeatherDataService
    {
        List<WeatherDataModel> GetAllWeatherDataByCityName(string cityName);
        void AddWeatherData(WeatherDataModel weatherData);
        void UpdateWeatherData(string cityName, WeatherDataModel updatedData);
        void DeleteCityAndWeatherData(string cityName);
    }
}

