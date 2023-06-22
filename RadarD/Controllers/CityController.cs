using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadarD.BLL.Data;
using RadarD.BLL.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RadarD.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        { 
        
            _cityService = cityService;
        }

        [HttpGet]
        [Route("api/v1/City")]
        public IEnumerable<CityDTO> GetCities()
        {
            return _cityService.GetCities();
        }

        
        [HttpGet]
        [Route("api/v2/City"), AllowAnonymous]
        public IEnumerable<CityDTO> GetCitiesDummy()
        {
            return _cityService.GetCities();
        }
        /*
                // GET api/<CityController>/5
                [HttpGet("{id}")]
                public string Get(int id)
                {
                    return "value";
                }

                // POST api/<CityController>
                [HttpPost]
                public void Post([FromBody] string value)
                {
                }

                // PUT api/<CityController>/5
                [HttpPut("{id}")]
                public void Put(int id, [FromBody] string value)
                {
                }

                // DELETE api/<CityController>/5
                [HttpDelete("{id}")]
                public void Delete(int id)
                {
                }
        */
    }
}
