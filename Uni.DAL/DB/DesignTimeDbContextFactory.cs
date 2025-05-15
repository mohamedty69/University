using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Uni.DAL.DB
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using System.Reflection;

    namespace Uni.DAL.DB
    {
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                // Get the executing assembly's directory
                var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                // Navigate up to the web project (adjust if your structure is different)
                var webProjectPath = Path.GetFullPath(Path.Combine(basePath, "../Uni.PLL"));

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(@"C:\Users\Rovan Hussien\source\repos\University\Uni.PLL")// Point to web project directory
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Could not find DefaultConnection in appsettings.json");
                }

                var builder = new DbContextOptionsBuilder<AppDbContext>();
                builder.UseSqlServer(connectionString);

                return new AppDbContext(builder.Options);
            }
        }
    }
}
