using System;
using System.Collections.Generic;
using System.Linq;

namespace RxNLizer
{
    /// <summary>
    /// Provides tools for extracting and comapring tokens from free text
    /// </summary>
    public class TokenAnalyser
    {
        /// <summary>
        /// Array of words not be used as candidate if it appears in sequential checking
        /// </summary>
        private static string[] blackListArr = new string[] { "alpha", "beta" };

        /// <summary>
        /// Converts Rx free text to Token Array
        /// </summary>
        /// <returns></returns>
        public static string[] ToTokenArray(string text)
        {
            return text.Split(' ');
        }
        
        /// <summary>
        /// Check if not in blacklist, which represents tokens which are not discriminatory
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool IsNotInBlackList(string token)
        {            
            return (blackListArr.Contains(token, StringComparer.OrdinalIgnoreCase) == false) ? true : false; 
        }

        /// <summary>
        /// Selects candidate token from Rx free text for matching against RxNorm
        /// </summary>
        /// <param name="tokenArray"></param>
        /// <returns></returns>
        public static string SelectCandidateToken(string[] tokenArray)
        {            
            string ret = string.Empty;

            if (tokenArray !=null && tokenArray.Length>0)
            {
                for (int i = 0; i < tokenArray.Length; i++)
                {
                    if (IsNotInBlackList(tokenArray[i]))
                    {
                        ret = tokenArray[i];
                        break;
                    }
                }
            }
            return ret;

        }
        /// <summary>
        /// Selects candidate RxCui from RxNorm.
        /// Ranking is based on number of matched tokens and compatibility with length
        /// </summary>
        /// <param name="rxn"></param>
        /// <param name="rxArr"></param>
        /// <returns></returns>

        public static List<Rxnconso> RxNormSTRMatchCount(IEnumerable<Rxnconso> rxn, string[] rxArr)
        {
            List<Rxnconso> r = new List<Rxnconso>();

            foreach (var rxConso in rxn)
            {
                int count = 0;
                
                for (int i = 0; i < rxArr.Length; i++)
                {
                    if (rxConso.Str.IndexOf(rxArr[i].ToUpper()) != -1)
                    {
                        count++;
                    }
                }

                rxConso.ElementCount = count;

                rxConso.LengthDifference = Math.Abs( rxConso.Str.Length - string.Join(" ", rxArr).Length);

                r.Add(rxConso);
            }

            
            return r;

        }
    }
}
