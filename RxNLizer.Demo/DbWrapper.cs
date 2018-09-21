using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxNLizer.Demo
{
    public static class DbWrapper
    {
        private static readonly string TkcConnection = ConfigurationManager.ConnectionStrings["TkcDbcontext"].ToString();

        public static void UpdateRxClass(string rx_text, string rx_classJson)
        {
            if (!(string.IsNullOrEmpty(rx_text) || string.IsNullOrEmpty(rx_classJson)))
            {
                string sql = "DECLARE @rxtext NVARCHAR(300)='" + rx_text + "'";

                sql += "DECLARE @rxclassJson NVARCHAR(4000)='" + rx_classJson + "'";

                sql += @"IF (NOT EXISTs(SELECT * FROM tkc_registry.coding_atc WHERE rx_text=@rxtext))
	                    BEGIN
		                    INSERT INTO tkc_registry.coding_atc(rx_text,RxClassMeta)
		                    VALUES(@rxtext, @rxclassJson)
	                    END
                    ELSE
	                    BEGIN
		                    UPDATE tkc_registry.coding_atc SET RxClassMeta = @rxclassJson WHERE rx_text=@rxtext
	                    END
                    ";

                using (var con = new SqlConnection(TkcConnection))
                {
                    try
                    {
                        var ret = con.Execute(sql);
                    }
                    catch (Exception)
                    {
                        System.Diagnostics.Debug.WriteLine("SQL exception");                        
                    }
                    

                    System.Diagnostics.Debug.WriteLine("write to table");
                    
                }
            }

            

        }

        public static IEnumerable<string> GetRxTexts()
        {
            IEnumerable<string> ret = new List<string>();

            string sql = @"SELECT rx_text FROM [TKC1].[tkc_registry].[coding_atc] WHERE Id>1 AND Id<10";

            
            using (var con = new SqlConnection(TkcConnection))
            {
                var o = con.Query<string>(sql);

                if (o!=null)
                {
                    ret = o;

                    Console.WriteLine(o.Aggregate((a,b)=> a+ "\r\n" + b));
                }


                System.Diagnostics.Debug.WriteLine("selecting from table");
                System.Diagnostics.Debug.WriteLine(ret);
            }

            return ret;
        }
    }
}
