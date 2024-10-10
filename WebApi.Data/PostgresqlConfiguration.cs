using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class PostgresqlConfiguration
    {
        public string Connection { get; set; } 


        public PostgresqlConfiguration(string connection) => Connection = connection;
        
    }
}
