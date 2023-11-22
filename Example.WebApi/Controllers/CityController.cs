using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Example.Service.Common;
using Microsoft.AspNetCore.Cors;
using System.Web.Http.Cors;
using Example.Model;
using Example.Repository;
using Example.Service;
using EnableCorsAttribute = System.Web.Http.Cors.EnableCorsAttribute;

namespace Example.WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]

    [RoutePrefix("api/city")]
    public class CityController : ApiController
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllCities()
        {
            try
            {
                var cities = _cityService.GetAllCities();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
