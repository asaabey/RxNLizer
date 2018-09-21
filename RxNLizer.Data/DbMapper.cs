using Dapper;
using RxNLizer.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace RxNLizer.Data
{
    public class DbMapper
    {
        public DbMapper()
        {

        }
        
        

        private string ConnectionString = ConfigurationManager.ConnectionStrings["Dbcontext"].ToString();

        public IEnumerable<Rxnconso> GetMatches(string term)
        {
            
            string sql = "SELECT [RXCUI] ,[STR]  FROM [Rxnorm].[dbo].[RXNCONSO]  WHERE  SAB='RXNORM' AND [STR] LIKE CONCAT('%',@term,'%')";

            IEnumerable<Rxnconso> ret = new List<Rxnconso>();

            using (var con = new SqlConnection(ConnectionString))
            {
                ret = con.Query<Rxnconso>(sql, new { term }).ToList();
                
            }

            return ret;
        }

    }
}
