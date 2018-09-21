using Dapper;
using RxNLizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace RxNLizer
{
    public class DbMapper
    {

        private readonly string RxNormConnection = ConfigurationManager.ConnectionStrings["RxNormDbcontext"].ToString();
        

        public IEnumerable<Rxnconso> GetMatches(string term)
        {
           
            string sql = "SELECT DISTINCT [RXCUI] as rxcui,UPPER([STR]) AS str  FROM [Rxnorm].[dbo].[RXNCONSO] WHERE [STR] LIKE '%" + term + "%' AND SAB='RXNORM'";

            IEnumerable<Rxnconso> ret = new List<Rxnconso>();

            using (var con = new SqlConnection(RxNormConnection))
            {
                ret = con.Query<Rxnconso>(sql);
            }

            return ret;
        }

        



    }
}
