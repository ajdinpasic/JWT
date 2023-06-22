using RadarD.DAL.Context;
using RadarD.DAL.Data;
using RadarD.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarD.DAL.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly MainDBContext _dbContext;

        public CityRepository(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<City> GetCities()
        {
            var cities = _dbContext.City.AsQueryable();
            return cities;
        }
    }
}
