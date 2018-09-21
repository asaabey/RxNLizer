using Dapper;
using RxNLizer.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace RxNLizer.DAL
{
    public class DbMapper
    {
        public DbMapper()
        {

        }
        
        //private string ConnectionString = "Data Source=.\\;Initial Catalog=Rxnorm;Integrated Security=True";

        private string ConnectionString = ConfigurationManager.ConnectionStrings["Dbcontext"].ToString();

        public IEnumerable<Rxnconso> GetMatches(string term)
        {
            string sql = "SELECT [RXCUI] ,[STR]  FROM [Rxnorm].[dbo].[RXNCONSO]  WHERE [STR] LIKE '%@searchterm%' AND SAB='RXNORM';";

            IEnumerable<Rxnconso> ret = new List<Rxnconso>();

            using (var con = new SqlConnection(ConnectionString))
            {
                ret = con.Query<Rxnconso>(sql, new { searchterm = term }).ToList();
            }

            return ret;
        }

    }
}
