using RadarD.BLL.Data;
using RadarD.BLL.Interface;
using RadarD.DAL.Interface;
using AutoMapper;
namespace RadarD.BLL.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public IEnumerable<CityDTO> GetCities()
        {
            var cities = _cityRepository.GetCities().AsEnumerable();
            return _mapper.Map<IEnumerable<CityDTO>>(cities);
                
        }
    }
}
