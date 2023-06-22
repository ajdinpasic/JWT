using Microsoft.EntityFrameworkCore;
using RadarD.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarD.DAL.Context
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions<MainDBContext> options) : base(options)
        {
             
        }

        public DbSet<City> City { get; set; }
        public DbSet<User> User { get; set; }
    }
}
