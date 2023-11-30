using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.Sql;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Using_Dapper_Core_API.Context
{
    public class DapperContext
    {
        private readonly IConfiguration iconfiguration;
        private readonly string _connectionstring;


        public DapperContext(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
            _connectionstring = iconfiguration.GetConnectionString("Myconn");
        }

        public IDbConnection createconnection()
            => new SqlConnection(_connectionstring);

        
    }
}
