using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace RxNLizer
{
    public class RxNLizer
    {

        /// <summary>
        /// Returns the prefered RxCui from RxNorm from a free text Rx 
        /// </summary>
        /// <param name="rx_text"></param>
        /// <returns></returns>

        private string  GetPreferredRxcuiFromText(string rx_text)
        {
            string returnString = string.Empty;

            if (!string.IsNullOrEmpty(rx_text))
            {
                string[] rx_textArray = TokenAnalyser.ToTokenArray(rx_text);

                if (rx_textArray.Length>0)
                {
                    string rx_candiate = TokenAnalyser.SelectCandidateToken(rx_textArray);

                    if (!string.IsNullOrEmpty(rx_candiate))
                    {
                        IEnumerable<Rxnconso> rxconsos_prelim = GetRxnconsosFromLocalDb(rx_candiate);

                        if (rxconsos_prelim!=null)
                        {
                            IEnumerable<Rxnconso> rxconsos_ranked = TokenAnalyser.RxNormSTRMatchCount(rxconsos_prelim, rx_textArray)
                                                                    .OrderBy(o => o.LengthDifference)
                                                                    .OrderByDescending(o => o.ElementCount);

                            if (rxconsos_ranked.FirstOrDefault()!=null)
                            {
                                string rxCui_candidate = rxconsos_ranked.FirstOrDefault().Rxcui;

                                if (!string.IsNullOrEmpty(rxCui_candidate))
                                {
                                    returnString = rxCui_candidate;
                                }
                                else
                                {
                                    NotifyStdOut("Exception: RxNorm candidate not determined");
                                }
                            }

                            
                        }
                        else
                        {
                            NotifyStdOut("Exception: No match found in RxNorm");
                        }
                    }
                    else
                    {
                        NotifyStdOut("Exception: Valid candidate token could not be found");
                    }
                }
                else
                {
                    NotifyStdOut("Exception: string cannot be tokenized");
                }
            }
            else
            {
                NotifyStdOut("Exception: Empty string provided");
            }

            

            return returnString;

            
        }


        /// <summary>
        /// Returns collection of RxConso from Local DB
        /// Dependent on Dapper.net
        /// </summary>
        /// <param name="rx_candidate"></param>
        /// <returns>
        /// Collection of matched Rxncoso objects
        /// </returns>
        private IEnumerable<Rxnconso> GetRxnconsosFromLocalDb(string rx_candidate)
        {
            DbMapper db = new DbMapper();

            return db.GetMatches(rx_candidate).ToList();
        }

        private void NotifyStdOut(string nofication)
        {
            System.Diagnostics.Debug.WriteLine("RxNLizer ->{0}", nofication);
        }

        


        /// <summary>
        /// Returns JSON string of RxClass return object
        /// 
        /// </summary>
        /// <param name="rx"></param>
        /// <returns>
        /// String of RxClass return Object in JSON
        /// </returns>
        public string GetRxClasses(string rx)
        {
            string ret = string.Empty;

            if (!string.IsNullOrEmpty(rx))
            {
                string preferredRxcui = GetPreferredRxcuiFromText(rx);

                if (!string.IsNullOrEmpty(preferredRxcui))
                {
                    RxClassReturnObject apiResultObj = RxClassApi.GetRxClass(int.Parse(preferredRxcui))
                        .GetAwaiter()
                        .GetResult();

                    if (apiResultObj!=null)
                    {
                        ret = JsonConvert.SerializeObject(apiResultObj);
                    }
                }

            }

            return ret;
        }

    }
}
