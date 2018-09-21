using Newtonsoft.Json;
using RxNLizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RxNLizer.RESTApi
{
    public class RxClassApi
    {
        public static async Task<RxClassReturnObject> GetRxClass(int rxCuiId)
        {
            string url = String.Format("https://rxnav.nlm.nih.gov/REST/rxclass/class/byRxcui.json?rxcui={0}&relaSource=VA", rxCuiId);
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    var retObj = JsonConvert.DeserializeObject<RxClassReturnObject>(result);

                    //var retObj2 = JsonConvert.DeserializeObject<dynamic>(result);

                    //var ret = retObj2.rxclassDrugInfoList.rxclassDrugInfo;

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
