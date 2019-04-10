using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWeather
{
    public class WeatherDbContext : DbContext
    {
        public DbSet<Weather> Weathers { get; set; }
    }
}
