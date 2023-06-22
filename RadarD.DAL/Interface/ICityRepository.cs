using RadarD.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarD.DAL.Interface
{
    public interface ICityRepository
    {
        IQueryable<City> GetCities();
    }
}
