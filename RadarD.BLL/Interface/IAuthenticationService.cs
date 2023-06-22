using RadarD.BLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarD.BLL.Interface
{
    public interface IAuthenticationService
    {
        void RegisterUser(UserDTO user);
        string LoginUser(UserDTO user);
        void LogoutUser(string jwt);
    }
}
