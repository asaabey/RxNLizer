using Newtonsoft.Json;
using RxNLizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RxNLizer
{
    public class RxClassApi
    {
        /// <summary>
        /// Gets JSON object from RxClass RESTFul API
        /// Uses additional filter for ATC codes only
        /// </summary>
        /// <param name="rxCuiId"></param>
        /// <returns></returns>
        public static async Task<RxClassReturnObject> GetRxClass(int rxCuiId)
        {
            string url = String.Format("https://rxnav.nlm.nih.gov/REST/rxclass/class/byRxcui.json?rxcui={0}&relaSource=ATC", rxCuiId);
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    var retObj = JsonConvert.DeserializeObject<RxClassReturnObject>(result);

                    return await  Task.FromResult(retObj);
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
