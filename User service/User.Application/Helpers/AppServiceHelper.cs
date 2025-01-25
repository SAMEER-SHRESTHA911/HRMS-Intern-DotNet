using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.Helpers
{
    public static class AppServiceHelper
    {
        private static IConfiguration _configuration;
        public static IConfiguration Configuration => _configuration;
        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    }
}
