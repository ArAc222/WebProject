using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Example.Model;
using Example.Repository;
using Example.Service;
using Example.Service.Common;

namespace Example.WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class WeatherDataController : ApiController
    {
        private readonly IWeatherDataService _weatherDataService;

        public WeatherDataController()
        {
            _weatherDataService = new WeatherDataService(new WeatherDataRepository());
        }

        public WeatherDataController(IWeatherDataService weatherDataService)
        {
            _weatherDataService = weatherDataService;
        }

        [HttpGet]
        [Route("api/weatherdata/bycity/{cityName}")]
        public IHttpActionResult GetWeatherDataByCity(string cityName)
        {
            try
            {
                var weatherData = _weatherDataService.GetAllWeatherDataByCityName(cityName);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/weatherdata/add")]
        public IHttpActionResult PostWeatherData([FromBody] WeatherDataModel weatherData)
        {
            try
            {
                _weatherDataService.AddWeatherData(weatherData);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("api/weatherdata/update/{cityName}")]
        public IHttpActionResult PutWeatherData(string cityName, [FromBody] WeatherDataModel updatedData)
        {
            try
            {
                _weatherDataService.UpdateWeatherData(cityName, updatedData);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("api/weatherdata/delete/{cityName}")]
        public IHttpActionResult DeleteWeatherData(string cityName)
        {
            try
            {
                _weatherDataService.DeleteCityAndWeatherData(cityName);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
