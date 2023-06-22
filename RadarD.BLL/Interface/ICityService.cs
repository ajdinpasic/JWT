using RadarD.BLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarD.BLL.Interface
{
    public interface ICityService
    {
        IEnumerable<CityDTO> GetCities();
    }
}
